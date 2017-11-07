using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsultationAppointmentRequestCalendar.Models
{
    public class ViewModel
    {
        public Physician FilteredPhysician { get; set; }
        public IList<Appointment> FilteredAppointment { get; set; }
        public IList<Appointment> AppointmentViewList { get; set; }
        public IList<Patient> PatientViewList { get; set; }
        public IList<Physician> PhysicianViewList { get; set; }

        public ViewModel()
        {
            PatientViewList = new List<Patient>();
            PhysicianViewList = new List<Physician>();
        }
        

    }
}