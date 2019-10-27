using Data.Model;
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
        public static class productPurchase
        {
            public static IQueryable<ProductsView> GetProductsFromInvoice(int p_id_invoice)
            {
                var r =
                    from i in _db.INVOICES_PURCHASES
                    join
                    ps in _db.PRODUCTS_PURCHASES on i.ID equals ps.ID_INVOICES_PURCHASES
                    join
                    p in _db.PRODUCTS on ps.ID_PRODUCTS equals p.ID
                    where i.ID == p_id_invoice
                    select new ProductsView
                    {
                        NAME = p.NAME,
                        CODE = p.CODE,
                        ID_PRODUCT = p.ID,
                        MONEY_UNIT = ps.MONEY_UNIT,
                        TAX_PERCE = ps.TAX_PERCE,
                        STAMP = ps.STAMP,
                        TAX_VALUE = (ps.TAX_PERCE * ps.MONEY_UNIT) / 100,
                        QUANTITY = ps.QUANTITY,
                        MONEY_PAID = (ps.MONEY_UNIT + (ps.TAX_PERCE * ps.MONEY_UNIT) / 100 + ps.STAMP) * ps.QUANTITY,
                        ID = ps.ID,
                    };
                return r;
            }
            public static void AddProductPurchase(int p_id_product, int p_id_invoice, double p_quantity = 1)
            {
                var invoice = InvoicePurchase.Get(p_id_invoice);
                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }

                if (IsExistProductInInvoice(p_id_product, p_id_invoice)) { DialogError.Error(); return; }
                if (p_quantity < 0) { DialogError.Error(); return; }

                var p = product.Get(p_id_product);
                double p_MONEY = p.MONEY_PURCHASE;
                double p_QUANTITY = p_quantity;
                double p_TAX_PERCE = p.TAX_PERCE;
                double p_STAMP = 0;
                double p_TAX_VALUE = p.TAX_PERCE / 100 * p.MONEY_PURCHASE;
                double MONEY_PAID = (p_MONEY + p_TAX_VALUE + p_STAMP) * p_QUANTITY;

                PRODUCTS_PURCHASES ps = new PRODUCTS_PURCHASES
                {
                    ID = NewId(),
                    ID_PRODUCTS = p_id_product,
                    ID_INVOICES_PURCHASES = p_id_invoice,
                    QUANTITY = p_QUANTITY,
                    MONEY_UNIT = p_MONEY,
                    TAX_PERCE = p_TAX_PERCE,
                    STAMP = 0
                };
                Add(ps);
            }
            public static bool IsExistProductInInvoice(int p_id_product, int p_id_invoice)
            {
                var r =
                    from i in _db.INVOICES_PURCHASES
                    join ps in _db.PRODUCTS_PURCHASES on i.ID equals ps.ID_INVOICES_PURCHASES
                    join p in _db.PRODUCTS on ps.ID_PRODUCTS equals p.ID
                    where i.ID == p_id_invoice && p.ID == p_id_product
                    select p;
                return (r.Count() > 0) ? true : false;
            }
            public static int NewId()
            {
                return (_db.PRODUCTS_PURCHASES.Count() > 0) ? _db.PRODUCTS_PURCHASES.Max(u => u.ID) + 1 : 1;
            }
            public static PRODUCTS_PURCHASES Get(int p_id)
            {
                return _db.PRODUCTS_PURCHASES.Single(c => c.ID == p_id);
            }

            public static void Delete(int p_id_ProductPurchase)
            {
                var ps = Get(p_id_ProductPurchase);
                var invoice = InvoicePurchase.Get((int)ps.ID_INVOICES_PURCHASES);

                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }

                _db.PRODUCTS_PURCHASES.Remove(ps);
                _db.SaveChanges();
            }

            public static void Add(PRODUCTS_PURCHASES p_ProductPurchase)
            {
                var invoice = InvoicePurchase.Get((int)p_ProductPurchase.ID_INVOICES_PURCHASES);

                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }

                _db.PRODUCTS_PURCHASES.Add(p_ProductPurchase);
                _db.SaveChanges();
            }

            //************************************************************************
            public static void Commit()
            {
                _db.SaveChanges();
            }
            public static void EditMoneyUnitOfProductPurchase(double p_MoneyUnit, int id_ProductPurchase)
            {
                if (p_MoneyUnit < 0) { DialogError.Error(); return; }

                var ps = Get(id_ProductPurchase);
                var invoice = InvoicePurchase.Get((int)ps.ID_INVOICES_PURCHASES);
                var p = product.Get((int)ps.ID_PRODUCTS);

                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }
                if (p_MoneyUnit < p.MONEY_PURCHASE) { DialogError.Error(); return; }

                ps.MONEY_UNIT = p_MoneyUnit;
                _db.SaveChanges();
            }
            public static void EditTaxPerceOfProductPurchase(double p_TaxPerce, int id_ProductPurchase)
            {
                if (p_TaxPerce < 0) { DialogError.Error(); return; }
                if (p_TaxPerce > 100) { DialogError.Error(); return; }

                var ps = Get(id_ProductPurchase);
                var invoice = InvoicePurchase.Get((int)ps.ID_INVOICES_PURCHASES);
                //var p = product.Get((int)ps.ID_PRODUCTS);

                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }

                ps.TAX_PERCE = p_TaxPerce;
                _db.SaveChanges();
            }
            public static void EditQuantityOfProductPurchase(double p_Quantity, int id_ProductPurchase)
            {
                if (p_Quantity < 0) { DialogError.Error(); return; }

                var ps = Get(id_ProductPurchase);
                var invoice = InvoicePurchase.Get((int)ps.ID_INVOICES_PURCHASES);
                var p = product.Get((int)ps.ID_PRODUCTS);

                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }

                ps.QUANTITY = p_Quantity;
                _db.SaveChanges();
            }
            public static void EditStampOfProductPurchase(double p_Stamp, int id_ProductPurchase)
            {
                if (p_Stamp < 0) { DialogError.Error(); return; }

                var ps = Get(id_ProductPurchase);
                var invoice = InvoicePurchase.Get((int)ps.ID_INVOICES_PURCHASES);
                //var p = product.Get((int)ps.ID_PRODUCTS);

                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }

                ps.STAMP = p_Stamp;
                _db.SaveChanges();

            }
        }
    }
}