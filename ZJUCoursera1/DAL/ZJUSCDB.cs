namespace ZJUCoursera1.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ZJUSCDB : DbContext
    {
        public ZJUSCDB()
            : base("name=ZJUSCDB")
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<SC> SCs { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Weekday> Weekdays { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(e => e.SCs)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.SCs)
                .WithRequired(e => e.Student)
                .HasForeignKey(e => e.SID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Subject)
                .HasForeignKey(e => e.SBID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Weekday>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Weekday)
                .HasForeignKey(e => e.WDID)
                .WillCascadeOnDelete(false);
        }
    }
}
