namespace AppointmentRequestWCF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class schedule_time_mapping
    {
        [Key]
        public int schedule_time_id { get; set; }

        public int? schedule_id { get; set; }

        public int? time_id { get; set; }

        public int? physician_id { get; set; }

        public virtual physician physician { get; set; }

        public virtual schedule schedule { get; set; }

        public virtual time time { get; set; }
    }
}
