using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultationAppointmentRequestCalendar.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public string Day { get; set; }
        public List<Time> Time { get; set; }
    }
}