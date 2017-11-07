namespace AppointmentRequestWCF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AppointmentRequestContext : DbContext
    {
        public AppointmentRequestContext()
            : base("name=AppointmentRequestContext")
        {
        }

        public virtual DbSet<appointment> appointments { get; set; }
        public virtual DbSet<patient> patients { get; set; }
        public virtual DbSet<physician> physicians { get; set; }
        public virtual DbSet<purpose> purposes { get; set; }
        public virtual DbSet<schedule> schedules { get; set; }
        public virtual DbSet<schedule_time_mapping> schedule_time_mapping { get; set; }
        public virtual DbSet<time> times { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<appointment>()
                .Property(e => e.remarks)
                .IsUnicode(false);

            modelBuilder.Entity<patient>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<patient>()
                .Property(e => e.middle_name)
                .IsUnicode(false);

            modelBuilder.Entity<patient>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<physician>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<physician>()
                .Property(e => e.middle_name)
                .IsUnicode(false);

            modelBuilder.Entity<physician>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<physician>()
                .Property(e => e.specialization)
                .IsUnicode(false);

            modelBuilder.Entity<purpose>()
                .Property(e => e.purpose1)
                .IsUnicode(false);

            modelBuilder.Entity<schedule>()
                .Property(e => e.day)
                .IsUnicode(false);
        }
    }
}
