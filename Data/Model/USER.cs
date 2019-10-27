namespace Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.USERS")]
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            INVOICES_PURCHASES = new HashSet<INVOICES_PURCHASES>();
            INVOICES_SELLS = new HashSet<INVOICES_SELLS>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(25)]
        public string NAME { get; set; }

        [StringLength(250)]
        public string DESCRIPTION { get; set; }

        [StringLength(25)]
        public string CODE { get; set; }

        [StringLength(25)]
        public string FIRSTNAME { get; set; }

        [StringLength(25)]
        public string LASTNAME { get; set; }

        [StringLength(25)]
        public string GENDER { get; set; }

        [StringLength(25)]
        public string ADDRESS { get; set; }

        [StringLength(25)]
        public string CITY { get; set; }

        [StringLength(25)]
        public string COUNTRY { get; set; }

        [StringLength(25)]
        public string PHONE { get; set; }

        [StringLength(25)]
        public string FAX { get; set; }

        [StringLength(25)]
        public string WEBSITE { get; set; }

        [StringLength(25)]
        public string EMAIL { get; set; }

        [StringLength(25)]
        public string PASSWORD { get; set; }

        public DateTime? BIRTHDAY { get; set; }

        public byte[] PICTURE { get; set; }

        public double MONEY_ACCOUNT { get; set; }

        public int TYPE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INVOICES_PURCHASES> INVOICES_PURCHASES { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INVOICES_SELLS> INVOICES_SELLS { get; set; }
    }
}
