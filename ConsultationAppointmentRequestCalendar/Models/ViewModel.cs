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
        private IList<Patient> _patientList;
        public IList<Patient> PatientViewList
        {
            get { return _patientList; }
            set
            {
                _patientList = _SeedData.PatientList;
            }
        }

        private IList<Physician> _physicianList;
        public IList<Physician> PhysicianViewList
        {
            get { return _physicianList; }
            set
            {
                _physicianList = _SeedData.PhysicianList;
            }
        }

        public ViewModel()
        {
            PatientViewList = new List<Patient>();
            PhysicianViewList = new List<Physician>();
        }
        

    }
}