namespace ZJUCoursera1.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Courses")]
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            SCs = new HashSet<SC>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int WDID { get; set; }

        [Required]
        [StringLength(50)]
        public string Time { get; set; }

        [Required]
        [StringLength(50)]
        public string Teacher { get; set; }

        [StringLength(50)]
        public string Intro { get; set; }

        public int Count { get; set; }

        public int Max { get; set; }

        public int SBID { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Weekday Weekday { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SC> SCs { get; set; }
    }
}
