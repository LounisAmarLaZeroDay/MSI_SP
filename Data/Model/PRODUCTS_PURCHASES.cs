namespace Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.PRODUCTS_PURCHASES")]
    public partial class PRODUCTS_PURCHASES
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? ID_PRODUCTS { get; set; }

        public int? ID_INVOICES_PURCHASES { get; set; }

        public double MONEY_UNIT { get; set; }

        public double QUANTITY { get; set; }

        public double TAX_PERCE { get; set; }

        public double STAMP { get; set; }

        public virtual INVOICES_PURCHASES INVOICES_PURCHASES { get; set; }

        public virtual PRODUCT PRODUCT { get; set; }
    }
}
