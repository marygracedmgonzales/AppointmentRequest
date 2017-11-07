namespace AppointmentRequestWCF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("appointment")]
    public partial class appointment
    {
        [Key]
        public int appointment_id { get; set; }

        public int? patient_id { get; set; }

        public int? physician_id { get; set; }

        public int? schedule_id { get; set; }

        public int? time_id { get; set; }

        public int? purpose_id { get; set; }

        [StringLength(250)]
        public string remarks { get; set; }

        public DateTime? date_time { get; set; }

        public virtual patient patient { get; set; }

        public virtual physician physician { get; set; }

        public virtual purpose purpose { get; set; }

        public virtual schedule schedule { get; set; }

        public virtual time time { get; set; }
    }
}
