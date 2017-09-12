using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultationAppointmentRequestCalendar.Models
{
    public class Physician
    {
        public int PhysicianId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public List<Schedule> Schedule { get; set; }
    }
}