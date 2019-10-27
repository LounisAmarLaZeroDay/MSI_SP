namespace Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Firebird.WHOLESALERS_TYPE")]
    public partial class WHOLESALERS_TYPE
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(25)]
        public string NAME { get; set; }

        [StringLength(25)]
        public string DESCRIPTION { get; set; }
    }
}
