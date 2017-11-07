using ConsultationAppointmentRequestCalendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultationAppointmentRequestCalendar.Adapter;

namespace ConsultationAppointmentRequestCalendar.Controllers
{
    public class PhysicianController : Controller
    {
        AppointmentServiceAdapter adapter = new AppointmentServiceAdapter();

        // GET: Physician
        public ActionResult Index()
        {
            return View(adapter.getPhysicians());
        }
    }
}