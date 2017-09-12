using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsultationAppointmentRequestCalendar.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime Date { get; set; }
        public int TimeId { get; set; }
        public int Purpose { get; set; }
        public string Remarks { get; set; }
    }
}