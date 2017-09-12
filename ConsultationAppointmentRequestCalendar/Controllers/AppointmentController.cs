using ConsultationAppointmentRequestCalendar.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ConsultationAppointmentRequestCalendar.Controllers
{
    public class AppointmentController : Controller
    {
        List<Appointment> AppointmentList = new List<Appointment>();
        
        public ActionResult Index()
        {
            return View(_SeedData.PhysicianList);
        }

        public ActionResult GetPhysician(int PhysicianId)
        {
            var doctor = _SeedData.PhysicianList.Where(s => s.PhysicianId == PhysicianId).FirstOrDefault();
            return Json(doctor, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAppointmentList()
        {
            using (StreamReader sr = new StreamReader(Server.MapPath("~/App_Data/Appointments.txt")))
            {
                AppointmentList = JsonConvert.DeserializeObject<List<Appointment>>(sr.ReadToEnd());
            }
            return Json(AppointmentList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetAppointment(int PhysicianId)
        {
            var doctor = _SeedData.PhysicianList.Where(s => s.PhysicianId == PhysicianId).FirstOrDefault();

            return View(doctor);
        }
        
        public ActionResult GetSchedule(int PhysicianId, DateTime SelectedDate)
        {
            var doctor = _SeedData.PhysicianList.Where(d => d.PhysicianId == PhysicianId).FirstOrDefault();
            var schedule = doctor.Schedule.Where(s => s.Day == Convert.ToString(SelectedDate.DayOfWeek)).FirstOrDefault();
            IList<Appointment> appointments = new List<Appointment>();
            Dictionary<string, Object> appointmentObject = new Dictionary<string, Object>();

            try
            {
                if (schedule != null)
                {
                    GetAppointmentList();
                    appointments = AppointmentList.Where(a => a.PhysicianId == PhysicianId && a.ScheduleId == schedule.ScheduleId && a.Date == SelectedDate).ToList();
                }
                
            }
            catch (ArgumentNullException e)
            {throw;}
            
            appointmentObject["schedule"] = schedule;
            appointmentObject["appointments"] = appointments;

            return Json(appointmentObject, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPatientList()
        {
            return Json(_SeedData.PatientList, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult SaveAppointment(Appointment AppointmentRequest)
        {
            GetAppointmentList();
            AppointmentRequest.AppointmentId = AppointmentList.OfType<Appointment>().LastOrDefault().AppointmentId + 1;
            AppointmentList.Add(AppointmentRequest);

            string json = JsonConvert.SerializeObject(AppointmentList.ToArray());
            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/Appointments.txt"), json);
            GetAppointmentList();

            return Json(AppointmentList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateAppointment(Appointment UpdatedRequest)
        {
            GetAppointmentList();
            int index = AppointmentList.FindIndex(a => a.AppointmentId == UpdatedRequest.AppointmentId);
            AppointmentList[index] = UpdatedRequest;
            
            string json = JsonConvert.SerializeObject(AppointmentList.ToArray());
            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/Appointments.txt"), json);
            GetAppointmentList();

            return Json(AppointmentList, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult DeleteAppointment(int toDeleteId)
        {
            GetAppointmentList();

            var appointmentToDelete = AppointmentList.Find(a => a.AppointmentId == toDeleteId);
            if (appointmentToDelete != null) AppointmentList.Remove(appointmentToDelete);
            
            string json = JsonConvert.SerializeObject(AppointmentList.ToArray());
            System.IO.File.WriteAllText(Server.MapPath("~/App_Data/Appointments.txt"), json);
            GetAppointmentList();

            return Json(AppointmentList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewAppointmentList()
        {
            GetAppointmentList();
            ViewModel model = new ViewModel();
            model.AppointmentViewList = AppointmentList;
            return View("ViewAppointmentList", model);
        }

        public ActionResult ViewPhysicianAppointmentList(int PhysicianId)
        {
            GetAppointmentList();
            ViewModel model = new ViewModel();
            model.AppointmentViewList = AppointmentList;

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