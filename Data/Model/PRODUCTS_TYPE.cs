namespace Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.PRODUCTS_TYPE")]
    public partial class PRODUCTS_TYPE
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(25)]
        public string NAME { get; set; }

        [StringLength(25)]
        public string DESCRIPTION { get; set; }
    }
}
