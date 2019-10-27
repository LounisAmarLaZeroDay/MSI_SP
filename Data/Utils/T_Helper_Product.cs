using Data.Model;
using LaZeroDayCore.Config;
using LaZeroDayCore.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Utils
{
    public static partial class  T_Helper
    {
        public static class product
        {
            static int PageCount = 0, PageSize = 0, LineCount = 0;
            static string LableSearch;
            public static IQueryable<PRODUCT> search(string p_srting, ref int PageThis, string p_orderBy)
            {
                if (p_srting == null) p_srting = "";
                IQueryable<PRODUCT> _Query;
                try
                {
                    try
                    {
                        _Query = null;
                        _Query = _db.PRODUCTS.Where(c => c.NAME.ToLower().Contains(p_srting) || c.DESCRIPTION.ToLower().Contains(p_srting)).OrderByDescending(p_orderBy);
                        LableSearch = SkipTake(ref PageThis, ref _Query);
                        return _Query;
                    }
                    catch
                    {
                        _Query = null;
                        _Query = _db.PRODUCTS.Where(c => c.NAME.ToLower().Contains(p_srting) || c.DESCRIPTION.ToLower().Contains(p_srting));
                        return _Query;
                    }

                }
                catch (Exception e) { F_File.LogError(e); return null; }
            }
            public static string SkipTake(ref int PageThis, ref IQueryable<PRODUCT> p_Query)
            {
                GetPageSize();
                if (PageSize <= 0) PageSize = 1;
                if (PageThis <= 0) PageThis = 0;
                if (PageThis > PageCount) PageThis = PageCount;
                LineCount = p_Query.Count();
                p_Query = p_Query.Skip(PageThis * PageSize).Take(PageSize);
                PageCount = (LineCount / PageSize);
                return string.Format("({0} / {1}) |{2}|", PageThis + 1, PageCount + 1, LineCount);
            }
            public static void GetPageSize()
            {
                PageSize = C_Variables.Software_.pageSizeSearch;
            }
            public static string GetLableSearch()
            {
                return LableSearch;
            }
            public static int NewId()
            {
                return (_db.PRODUCTS.Count() > 0) ? _db.PRODUCTS.Max(u => u.ID) + 1 : 1;
            }
            public static PRODUCT Get(int p_id)
            {
                return _db.PRODUCTS.Single(c => c.ID == p_id);
            }

            public static void Delete(int p_id)
            {
                _db.PRODUCTS.Remove(_db.PRODUCTS.Single(c => c.ID == p_id));
                _db.SaveChanges();
            }
            //************************************************************************
            public static void Commit()
            {
                _db.SaveChanges();
            }
            public static void EditFromObject(PRODUCT p)
            {
                if (p.NAME.Length == 0) { DialogError.Error(); return; }
                if (p.ID <= 0) if (IsExistName(p.NAME)) { DialogError.Error(); return; }
                if (p.ID > 0) if (!IsCanEditName(p.ID, p.NAME)) { DialogError.Error(); return; }

                if (p.CODE.Length > 0) if (p.ID <= 0) if (IsExistCode(p.CODE)) { DialogError.Error(); return; }
                if (p.CODE.Length > 0) if (p.ID > 0) if (!IsCanEditCode(p.ID, p.CODE)) { DialogError.Error(); return; }

                if (p.ID > 0) // edit
                {
                    try
                    {
                        var r = Get(p.ID);
                        r.NAME = p.NAME ?? "";
                        r.DESCRIPTION = p.DESCRIPTION ?? "";
                        r.CODE = p.CODE ?? "";
                        r.IMPORTANCE = p.IMPORTANCE;
                        r.QUANTITY = p.QUANTITY;
                        r.QUANTITY_MIN = p.QUANTITY_MIN;
                        r.TAX_PERCE = p.TAX_PERCE;
                        r.MONEY_PURCHASE = p.MONEY_PURCHASE;
                        r.MONEY_SELLING_1 = p.MONEY_SELLING_1;
                        r.MONEY_SELLING_2 = p.MONEY_SELLING_2;
                        r.MONEY_SELLING_3 = p.MONEY_SELLING_3;
                        r.MONEY_SELLING_4 = p.MONEY_SELLING_4;
                        r.MONEY_SELLING_5 = p.MONEY_SELLING_5;
                        r.MONEY_SELLING_MIN = p.MONEY_SELLING_MIN;
                        r.DATE_PRODUCTION = p.DATE_PRODUCTION;
                        r.DATE_PURCHASE = p.DATE_PURCHASE;
                        r.DATE_EXPIRATION = p.DATE_EXPIRATION;
                        r.PICTURE = p.PICTURE;
                        r.TYPE_UNITE = p.TYPE_UNITE;
                        r.TYPE = p.TYPE;
                        _db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        F_File.LogError(e);
                    }
                }
                else         // add
                {
                    try
                    {
                        p.ID = NewId();
                        _db.PRODUCTS.Add(p);
                        _db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        F_File.LogError(e);
                    }
                }
            }
            private static bool IsExistCode(string p_string)
            {
                return _db.PRODUCTS.Any(o => o.CODE == p_string);
            }
            private static bool IsCanEditCode(int id, string p_string)
            {
                return !_db.PRODUCTS.Any(o => o.CODE == p_string && o.ID != id);
            }
            private static bool IsExistName(string p_string)
            {
                return _db.PRODUCTS.Any(o => o.NAME == p_string);
            }
            private static bool IsCanEditName(int id, string p_string)
            {
                return !_db.PRODUCTS.Any(o => o.NAME == p_string && o.ID != id);
            }
            //*****************************************
        }
    }
}
