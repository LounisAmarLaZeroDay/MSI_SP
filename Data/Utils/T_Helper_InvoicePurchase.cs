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
        public class InvoicePurchase
        {
            static int PageCount = 0, PageSize = 0, LineCount = 0;
            static string LableSearch;
            public static IQueryable<INVOICES_PURCHASES> search(string p_srting, ref int PageThis, string p_orderBy
                , int p_id_user, int p_id_Wholesaler
                , DateTime p_dateTime_begin, DateTime p_dateTime_end)
            {
                if (p_srting == null) p_srting = "";
                IQueryable<INVOICES_PURCHASES> _Query;
                try
                {
                    try
                    {
                        _Query = null;
                        _Query = _db.INVOICES_PURCHASES.Where(c => c.DESCRIPTION.ToLower().Contains(p_srting)).OrderByDescending(p_orderBy);
                        if (p_id_user > 0) _Query = _Query.Where(u => u.ID_USERS == p_id_user);
                        if (p_id_Wholesaler > 0) _Query = _Query.Where(c => c.ID_WHOLESALERS == p_id_Wholesaler);
                        if ((p_dateTime_begin != null) && (p_dateTime_end != null)) _Query = _Query.Where(d => d.DATE >= p_dateTime_begin && d.DATE <= p_dateTime_end);
                        LableSearch = SkipTake(ref PageThis, ref _Query);
                        return _Query;
                    }
                    catch
                    {
                        _Query = null;
                        _Query = _db.INVOICES_PURCHASES.Where(c => c.DESCRIPTION.ToLower().Contains(p_srting));
                        if (p_id_user > 0) _Query = _Query.Where(u => u.ID_USERS == p_id_user);
                        if (p_id_Wholesaler > 0) _Query = _Query.Where(c => c.ID_WHOLESALERS == p_id_Wholesaler);
                        if ((p_dateTime_begin != null) && (p_dateTime_end != null)) _Query = _Query.Where(d => d.DATE >= p_dateTime_begin && d.DATE <= p_dateTime_end);
                        LableSearch = SkipTake(ref PageThis, ref _Query);
                        return _Query;
                    }

                }
                catch (Exception e) { F_File.LogError(e); return null; }
            }
            public static string SkipTake(ref int PageThis, ref IQueryable<INVOICES_PURCHASES> p_Query)
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
            public static void Calc(int p_invoice_id)
            {
                var mInvoicePurchase = Get(p_invoice_id);
                var productsViews = productPurchase.GetProductsFromInvoice(p_invoice_id).ToList();

                double money_without_addedd = 0f;
                double money_tax = 0f;
                double money_stamp = 0f;
                double money_total = 0f;

                foreach (object o in productsViews)
                {
                    double MONEY_UNIT = F_File.GetPropertyDouble(o, "MONEY_UNIT");
                    double TAX_PERCE = F_File.GetPropertyDouble(o, "TAX_PERCE");
                    double TAX_VALUE = F_File.GetPropertyDouble(o, "TAX_VALUE");
                    double STAMP = F_File.GetPropertyDouble(o, "STAMP");
                    double MONEY_PAID = F_File.GetPropertyDouble(o, "MONEY_PAID");

                    money_without_addedd = +MONEY_UNIT;
                    money_tax = +TAX_VALUE;
                    money_stamp = +STAMP;
                    money_total = +MONEY_PAID;
                }

                mInvoicePurchase.MONEY_WITHOUT_ADDEDD = money_without_addedd;
                mInvoicePurchase.MONEY_TAX = money_tax;
                mInvoicePurchase.MONEY_STAMP = money_stamp;
                mInvoicePurchase.MONEY_TOTAL = money_total;
                mInvoicePurchase.DATE = DateTime.Now;
                _db.SaveChanges();
            }
            public static void Validate(int p_invoice_id, double p_mony_paid, string p_description)
            {
                var mInvoicePurchase = Get(p_invoice_id);
                if (mInvoicePurchase.VALIDATION == 1) { DialogError.Error(); return; }

                var productsViews = productPurchase.GetProductsFromInvoice(p_invoice_id).ToList();
                foreach (object o in productsViews)
                {
                    double QUANTITY = F_File.GetPropertyDouble(o, "QUANTITY");
                    int ID_PRODUCT = F_File.GetPropertyInt(o, "ID_PRODUCT");
                    var p = product.Get(ID_PRODUCT);
                    if ((p.QUANTITY - QUANTITY) < 0) { DialogError.Error(); return; }
                    p.QUANTITY = p.QUANTITY - QUANTITY;
                }

                mInvoicePurchase.MONEY_PAID = p_mony_paid;
                mInvoicePurchase.MONEY_UNPAID = mInvoicePurchase.MONEY_TOTAL - p_mony_paid;
                mInvoicePurchase.DESCRIPTION = p_description;
                mInvoicePurchase.VALIDATION = 1;

                if (mInvoicePurchase.MONEY_PAID < mInvoicePurchase.MONEY_TOTAL)
                {
                    var c = wholesaler.Get((int)mInvoicePurchase.ID_USERS);
                    c.MONEY_ACCOUNT = c.MONEY_ACCOUNT - mInvoicePurchase.MONEY_UNPAID;
                }

                _db.SaveChanges();
            }
            public static void ValidateCancel(int p_invoice_id)
            {
                var mInvoicePurchase = Get(p_invoice_id);
                if (mInvoicePurchase.VALIDATION != 1) { DialogError.Error(); return; }

                var productsViews = productPurchase.GetProductsFromInvoice(p_invoice_id).ToList();
                foreach (object o in productsViews)
                {
                    double QUANTITY = F_File.GetPropertyDouble(o, "QUANTITY");
                    int ID_PRODUCT = F_File.GetPropertyInt(o, "ID_PRODUCT");
                    var p = product.Get(ID_PRODUCT);
                    p.QUANTITY = p.QUANTITY + QUANTITY;
                }

                if (mInvoicePurchase.MONEY_PAID < mInvoicePurchase.MONEY_TOTAL)
                {
                    var c = wholesaler.Get((int)mInvoicePurchase.ID_USERS);
                    c.MONEY_ACCOUNT = c.MONEY_ACCOUNT + mInvoicePurchase.MONEY_UNPAID;
                }

                mInvoicePurchase.MONEY_PAID = 0;
                mInvoicePurchase.MONEY_UNPAID = 0;
                mInvoicePurchase.VALIDATION = 0;

                _db.SaveChanges();
            }
            public static INVOICES_PURCHASES AddInvoice(int id_user, int id_Wholesaler)
            {
                try
                {
                    var mInvoices = new INVOICES_PURCHASES
                    {
                        ID = NewId(),
                        ID_USERS = id_user,
                        ID_WHOLESALERS = id_Wholesaler,
                        DATE = DateTime.Now,
                        DESCRIPTION = "",
                        VALIDATION = 0,
                        MONEY_TAX = 0,
                        MONEY_STAMP = 0,
                        MONEY_WITHOUT_ADDEDD = 0,
                        MONEY_TOTAL = 0,
                        MONEY_PAID = 0,
                        MONEY_UNPAID = 0,
                    };
                    Add(mInvoices);
                    return mInvoices;
                }
                catch { return null; }
            }
            public static int NewId()
            {
                return (_db.INVOICES_PURCHASES.Count() > 0) ? _db.INVOICES_PURCHASES.Max(u => u.ID) + 1 : 1;
            }
            public static int Count()
            {
                return _db.INVOICES_PURCHASES.Count();
            }
            public static DateTime GetBeginDate()
            {
                return (DateTime)_db.INVOICES_PURCHASES.Min(d => d.DATE);
            }
            public static DateTime GetEndDate()
            {
                return (DateTime)_db.INVOICES_PURCHASES.Max(d => d.DATE);
            }
            public static INVOICES_PURCHASES Get(int p_id)
            {
                return _db.INVOICES_PURCHASES.Single(c => c.ID == p_id);
            }

            public static void Delete(int id_invoice)
            {
                var invoice = Get(id_invoice);
                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }

                _db.INVOICES_PURCHASES.Remove(invoice);
                _db.SaveChanges();
            }
            public static void Add(INVOICES_PURCHASES p)
            {
                _db.INVOICES_PURCHASES.Add(p);
                _db.SaveChanges();
            }
            //************************************************************************
            public static void Commit()
            {
                _db.SaveChanges();
            }
            public static void EditFromObject(INVOICES_PURCHASES p)
            {
                if (p.VALIDATION == 1) { DialogError.Error(); return; }

                if (p.ID > 0) // edit
                {
                    try
                    {
                        var r = Get(p.ID);
                        r.DATE = p.DATE;
                        r.DESCRIPTION = p.DESCRIPTION;
                        r.ID_WHOLESALERS = p.ID_WHOLESALERS;
                        r.ID_USERS = p.ID_USERS;
                        r.MONEY_PAID = p.MONEY_PAID;
                        r.MONEY_STAMP = p.MONEY_STAMP;
                        r.MONEY_TAX = p.MONEY_TAX;
                        r.MONEY_TOTAL = p.MONEY_TOTAL;
                        r.MONEY_UNPAID = p.MONEY_UNPAID;
                        r.MONEY_WITHOUT_ADDEDD = p.MONEY_WITHOUT_ADDEDD;
                        r.VALIDATION = p.VALIDATION;
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
                        _db.INVOICES_PURCHASES.Add(p);
                        _db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        F_File.LogError(e);
                    }
                }
            }
            public static void EditUserOfInvoice(int id_user, int id_invoice)
            {
                var invoice = Get(id_invoice);
                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }

                invoice.ID_USERS = id_user;
                _db.SaveChanges();
            }
            public static void EditWholesalerOfInvoice(int id_Wholesaler, int id_invoice)
            {
                var invoice = Get(id_invoice);
                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }

                invoice.ID_WHOLESALERS = id_Wholesaler;
                _db.SaveChanges();
            }
        }
    }
}