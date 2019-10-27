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
    public static partial class T_Helper
    {
        public static class customer
        {
            static int PageCount = 0, PageSize = 0, LineCount = 0;
            static string LableSearch;
            public static IQueryable<CUSTOMER> search(string p_srting, ref int PageThis, string p_orderBy)
            {
                if (p_srting == null) p_srting = "";
                IQueryable<CUSTOMER> _Query;
                try
                {
                    try
                    {
                        _Query = null;
                        _Query = _db.CUSTOMERS.Where( c => c.NAME.ToLower().Contains(p_srting) || c.DESCRIPTION.ToLower().Contains(p_srting) ).OrderByDescending(p_orderBy);
                        LableSearch = SkipTake(ref PageThis, ref _Query);
                        return _Query;
                    }
                    catch
                    {
                        _Query = null;
                        _Query = _db.CUSTOMERS.Where(c => c.NAME.ToLower().Contains(p_srting) || c.DESCRIPTION.ToLower().Contains(p_srting));
                        return _Query;
                    }

                }
                catch (Exception e) { F_File.LogError(e); return null; }
            }
            public static string SkipTake(ref int PageThis, ref IQueryable<CUSTOMER> p_Query)
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
                return (_db.CUSTOMERS.Count() > 0) ? _db.CUSTOMERS.Max(u => u.ID) + 1 : 1;
            }
            public static CUSTOMER Get(int p_id)
            {
                return _db.CUSTOMERS.Single(c => c.ID == p_id);
            }

            public static void Delete(int p_id)
            {
                _db.CUSTOMERS.Remove(_db.CUSTOMERS.Single(c => c.ID == p_id));
                _db.SaveChanges();
            }
            //************************************************************************
            public static void Commit()
            {
                _db.SaveChanges();
            }
            public static void EditFromObject(CUSTOMER p)
            {
                if (p.NAME.Length == 0) {DialogError.Error() ; return; }
                if (p.ID <= 0) if (IsExistName(p.NAME)) { DialogError.Error(); return; }
                if (p.ID > 0) if (!IsCanEditName(p.ID, p.NAME)) { DialogError.Error(); return; }

                if (p.CODE.Length > 0) if (p.ID <= 0) if (IsExistCode(p.CODE)) { DialogError.Error(); return; }
                if (p.CODE.Length > 0) if (p.ID > 0) if (!IsCanEditCode(p.ID, p.CODE)) { DialogError.Error(); return; }

                if (p.ID > 0) // edit
                {
                    try
                    {
                        var r = Get(p.ID);
                        r.NAME = p.NAME;
                        r.DESCRIPTION = p.DESCRIPTION;
                        r.CODE = p.CODE;
                        r.FIRSTNAME = p.FIRSTNAME;
                        r.LASTNAME = p.LASTNAME;
                        r.GENDER = p.GENDER;
                        r.BIRTHDAY = p.BIRTHDAY ;
                        r.ADDRESS = p.ADDRESS;
                        r.CITY = p.CITY;
                        r.COUNTRY = p.COUNTRY;
                        r.PHONE = p.PHONE;
                        r.FAX = p.FAX;
                        r.WEBSITE = p.WEBSITE;
                        r.EMAIL = p.EMAIL;
                        r.MONEY_ACCOUNT = p.MONEY_ACCOUNT;
                        r.PICTURE = p.PICTURE;
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
                        _db.CUSTOMERS.Add(p);
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
                return _db.CUSTOMERS.Any(o => o.CODE == p_string);
            }
            private static bool IsCanEditCode(int id, string p_string)
            {
                return !_db.CUSTOMERS.Any(o => o.CODE == p_string && o.ID != id);
            }
            private static bool IsExistName(string p_string)
            {
                return _db.CUSTOMERS.Any(o => o.NAME == p_string);
            }
            private static bool IsCanEditName(int id, string p_string)
            {
                return !_db.CUSTOMERS.Any(o => o.NAME == p_string && o.ID != id);
            }
            //*****************************************
        }
    }
}
