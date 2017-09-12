using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultationAppointmentRequestCalendar.Models
{
    public class Time
    {
        public int TimeId { get; set; }

        [Range(1, 24)]
        public int StartTime { get; set; }

        [Range(1, 24)]
        public int EndTime { get; set; }
    }
}