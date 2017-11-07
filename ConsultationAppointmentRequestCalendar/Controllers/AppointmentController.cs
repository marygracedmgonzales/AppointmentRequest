using ConsultationAppointmentRequestCalendar.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ConsultationAppointmentRequestCalendar.Adapter;

namespace ConsultationAppointmentRequestCalendar.Controllers
{
    public class AppointmentController : Controller
    {
        AppointmentServiceAdapter adapter = new AppointmentServiceAdapter();
               
        public ActionResult Index()
        {
            return View(adapter.getPhysicians());
        }
        
        public ActionResult GetPhysician(int PhysicianId)
        {
            return Json(adapter.getPhysician(PhysicianId), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult SetAppointment(int PhysicianId)
        {
            return View(adapter.setAppointment(PhysicianId));
        }
        
        public ActionResult GetSchedule(int PhysicianId, DateTime SelectedDate)
        {
            var doctor = adapter.getPhysician(PhysicianId);
            var schedule = doctor.Schedule.Where(s => s.Day == Convert.ToString(SelectedDate.DayOfWeek)).FirstOrDefault();
            IList<Appointment> appointments = new List<Appointment>();
            Dictionary<string, Object> appointmentObject = new Dictionary<string, Object>();

            try
            {
                if (schedule != null)
                {
                    List<Appointment> appointmentListDB = adapter.getAppointmentList();
                    if(appointmentListDB.Count() != 0)
                    {
                        appointments = appointmentListDB.Where(a => a.PhysicianId == PhysicianId && a.ScheduleId == schedule.ScheduleId && a.Date == SelectedDate).ToList();
                    }
                }
                
            }
            catch (ArgumentNullException ex)
            {throw ex;}
            
            appointmentObject["schedule"] = schedule;
            appointmentObject["appointments"] = appointments;

            return Json(appointmentObject, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetPatientList()
        {
            return Json(adapter.getPatientList(), JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult SaveAppointment(Appointment AppointmentRequest)
        {
            bool isSaved = false;
            List<Appointment> appointmentListDBUpdated = new List<Appointment>();

            isSaved = adapter.saveAppointment(AppointmentRequest);
            if (isSaved)
            {
                appointmentListDBUpdated = adapter.getAppointmentList();
            }
            return Json(appointmentListDBUpdated, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult UpdateAppointment(Appointment UpdatedRequest)
        {
            bool isUpdated = false;
            List<Appointment> appointmentListDBUpdated = new List<Appointment>();

            isUpdated = adapter.updateAppointment(UpdatedRequest);
            if(isUpdated)
            {
                appointmentListDBUpdated = adapter.getAppointmentList();
            }
            return Json(appointmentListDBUpdated, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult DeleteAppointment(int toDeleteId)
        {
            bool isDeleted = false;
            List<Appointment> appointmentListDBUpdated = new List<Appointment>();

            isDeleted = adapter.deleteAppointment(toDeleteId);
            if (isDeleted)
            {
                appointmentListDBUpdated = adapter.getAppointmentList();
            }
            return Json(appointmentListDBUpdated, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult ViewAppointmentList()
        {
            ViewModel model = new ViewModel();
            model.PatientViewList = adapter.getPatientList();
            model.PhysicianViewList = adapter.getPhysicians();
            model.AppointmentViewList = adapter.getAppointmentList();
            return View("ViewAppointmentList", model);
        }
        
        public ActionResult ViewPhysicianAppointmentList(int PhysicianId)
        {
            ViewModel model = new ViewModel();
            model.PatientViewList = adapter.getPatientList();
            model.PhysicianViewList = adapter.getPhysicians();
            model.AppointmentViewList = adapter.getAppointmentList();

            Physician physicianFiltered = new Physician();
            physicianFiltered = model.PhysicianViewList.Where(fp => fp.PhysicianId == PhysicianId).FirstOrDefault();
            model.FilteredPhysician = physicianFiltered;

            IList<Appointment> appointmentFiltered = new List<Appointment>();
            appointmentFiltered = model.AppointmentViewList.Where(fa => fa.PhysicianId == physicianFiltered.PhysicianId).ToList();
            model.FilteredAppointment = appointmentFiltered;

            return PartialView("_ViewPhysicianAppointmentList", model);
        }
    }
}