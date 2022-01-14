namespace ZJUCoursera1.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SC
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? SelectAt { get; set; }

        public virtual Course Course { get; set; }

        public virtual Student Student { get; set; }
    }
}
