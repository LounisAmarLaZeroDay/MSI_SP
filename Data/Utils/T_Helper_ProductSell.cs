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
        public static class productSell
        {
            public static IQueryable<ProductsView> GetProductsFromInvoice(int p_id_invoice)
            {
                var r =
                    from i in _db.INVOICES_SELLS
                    join
                    ps in _db.PRODUCTS_SELLS on i.ID equals ps.ID_INVOICES_SELLS
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
                        STAMP     = ps.STAMP,
                        TAX_VALUE = (ps.TAX_PERCE * ps.MONEY_UNIT) / 100,
                        QUANTITY = ps.QUANTITY,
                        MONEY_PAID = ( ps.MONEY_UNIT + (ps.TAX_PERCE * ps.MONEY_UNIT) / 100 + ps.STAMP ) * ps.QUANTITY,
                        ID = ps.ID,                   
                    };
                return r;
            }
            public static void AddProductSell(int p_id_product, int p_id_invoice, double p_quantity = 1)
            {
                var invoice = InvoiceSell.Get(p_id_invoice);
                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }

                if (IsExistProductInInvoice(p_id_product, p_id_invoice)) { DialogError.Error(); return; }
                if (p_quantity < 0) { DialogError.Error(); return; }

                var p = product.Get(p_id_product);
                double p_MONEY = p.MONEY_SELLING_1;
                double p_QUANTITY = p_quantity;
                double p_TAX_PERCE = p.TAX_PERCE;
                double p_STAMP = 0;
                double p_TAX_VALUE = p.TAX_PERCE / 100 * p.MONEY_SELLING_1;
                double MONEY_PAID = (p_MONEY + p_TAX_VALUE + p_STAMP) * p_QUANTITY;

                PRODUCTS_SELLS ps = new PRODUCTS_SELLS
                {
                    ID = NewId(),
                    ID_PRODUCTS = p_id_product,
                    ID_INVOICES_SELLS = p_id_invoice,
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
                    from i in _db.INVOICES_SELLS
                    join ps in _db.PRODUCTS_SELLS on i.ID equals ps.ID_INVOICES_SELLS
                    join p in _db.PRODUCTS on ps.ID_PRODUCTS equals p.ID
                    where i.ID == p_id_invoice && p.ID == p_id_product
                    select p;
                return (r.Count() > 0) ? true : false;
            }
            public static int NewId()
            {
                return (_db.PRODUCTS_SELLS.Count() > 0) ? _db.PRODUCTS_SELLS.Max(u => u.ID) + 1 : 1;
            }
            public static PRODUCTS_SELLS Get(int p_id)
            {
                return _db.PRODUCTS_SELLS.Single(c => c.ID == p_id);
            }

            public static void Delete(int p_id_ProductSell)
            {
                var ps = Get(p_id_ProductSell);
                var invoice = InvoiceSell.Get((int)ps.ID_INVOICES_SELLS);

                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }

                _db.PRODUCTS_SELLS.Remove(ps);
                _db.SaveChanges();
            }

            public static void Add(PRODUCTS_SELLS p_ProductSell, double p_Quantity = 1)
            {
                var invoice = InvoiceSell.Get((int)p_ProductSell.ID_INVOICES_SELLS);
                var p = product.Get((int)p_ProductSell.ID_PRODUCTS);

                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }
                if ((p.QUANTITY - p_Quantity) < 0) { DialogError.Error(); return; }

                _db.PRODUCTS_SELLS.Add(p_ProductSell);
                _db.SaveChanges();
            }

            //************************************************************************
            public static void Commit()
            {
                _db.SaveChanges();
            }
            public static void EditMoneyUnitOfProductSell(double p_MoneyUnit, int id_ProductSell)
            {
                if (p_MoneyUnit < 0) { DialogError.Error(); return; }

                var ps = Get(id_ProductSell);
                var invoice = InvoiceSell.Get((int)ps.ID_INVOICES_SELLS);
                var p = product.Get((int)ps.ID_PRODUCTS);

                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }
                if (p_MoneyUnit < p.MONEY_SELLING_MIN) { DialogError.Error(); return; }

                ps.MONEY_UNIT = p_MoneyUnit;
                _db.SaveChanges();
            }
            public static void EditTaxPerceOfProductSell(double p_TaxPerce, int id_ProductSell)
            {
                if (p_TaxPerce < 0) { DialogError.Error(); return; }
                if (p_TaxPerce > 100) { DialogError.Error(); return; }

                var ps = Get(id_ProductSell);
                var invoice = InvoiceSell.Get((int)ps.ID_INVOICES_SELLS);
                //var p = product.Get((int)ps.ID_PRODUCTS);

                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }

                ps.TAX_PERCE = p_TaxPerce;
                _db.SaveChanges();
            }
            public static void EditQuantityOfProductSell(double p_Quantity, int id_ProductSell)
            {
                if (p_Quantity < 0) { DialogError.Error(); return; }

                var ps = Get(id_ProductSell);
                var invoice = InvoiceSell.Get((int)ps.ID_INVOICES_SELLS);
                var p = product.Get((int)ps.ID_PRODUCTS);

                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }
                if ((p.QUANTITY - p_Quantity) < 0) { DialogError.Error(); return; }

                ps.QUANTITY = p_Quantity;
                _db.SaveChanges();
            }
            public static void EditStampOfProductSell(double p_Stamp, int id_ProductSell)
            {
                if (p_Stamp < 0) { DialogError.Error(); return; }

                var ps = Get(id_ProductSell);
                var invoice = InvoiceSell.Get((int)ps.ID_INVOICES_SELLS);
                //var p = product.Get((int)ps.ID_PRODUCTS);

                if (invoice.VALIDATION == 1) { DialogError.Error(); return; }

                ps.STAMP = p_Stamp;
                _db.SaveChanges();
            }
        }
    }
}
//Customer - Wholesaler
//Sell - Purchase