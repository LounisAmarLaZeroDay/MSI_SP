namespace Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.PRODUCTS")]
    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            PRODUCTS_PURCHASES = new HashSet<PRODUCTS_PURCHASES>();
            PRODUCTS_SELLS = new HashSet<PRODUCTS_SELLS>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(25)]
        public string NAME { get; set; }

        [StringLength(25)]
        public string DESCRIPTION { get; set; }

        [StringLength(25)]
        public string CODE { get; set; }

        public double IMPORTANCE { get; set; }

        public double QUANTITY { get; set; }

        public double QUANTITY_MIN { get; set; }

        public double TAX_PERCE { get; set; }

        public double MONEY_PURCHASE { get; set; }

        public double MONEY_SELLING_1 { get; set; }

        public double MONEY_SELLING_2 { get; set; }

        public double MONEY_SELLING_3 { get; set; }

        public double MONEY_SELLING_4 { get; set; }

        public double MONEY_SELLING_5 { get; set; }

        public double MONEY_SELLING_MIN { get; set; }

        public DateTime? DATE_PRODUCTION { get; set; }

        public DateTime? DATE_PURCHASE { get; set; }

        public DateTime? DATE_EXPIRATION { get; set; }

        public byte[] PICTURE { get; set; }

        public int TYPE { get; set; }

        public int TYPE_UNITE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTS_PURCHASES> PRODUCTS_PURCHASES { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTS_SELLS> PRODUCTS_SELLS { get; set; }
    }
}
