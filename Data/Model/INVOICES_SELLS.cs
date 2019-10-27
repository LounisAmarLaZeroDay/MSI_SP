namespace Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.INVOICES_SELLS")]
    public partial class INVOICES_SELLS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public INVOICES_SELLS()
        {
            PRODUCTS_SELLS = new HashSet<PRODUCTS_SELLS>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? ID_USERS { get; set; }

        public int? ID_CUSTOMERS { get; set; }

        [StringLength(25)]
        public string DESCRIPTION { get; set; }

        public DateTime? DATE { get; set; }

        public int VALIDATION { get; set; }

        public double MONEY_WITHOUT_ADDEDD { get; set; }

        public double MONEY_TAX { get; set; }

        public double MONEY_STAMP { get; set; }

        public double MONEY_TOTAL { get; set; }

        public double MONEY_PAID { get; set; }

        public double MONEY_UNPAID { get; set; }

        public virtual CUSTOMER CUSTOMER { get; set; }

        public virtual USER USER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTS_SELLS> PRODUCTS_SELLS { get; set; }
    }
}
