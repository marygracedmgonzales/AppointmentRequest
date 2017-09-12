using ConsultationAppointmentRequestCalendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsultationAppointmentRequestCalendar.Controllers
{
    public class PhysicianController : Controller
    {

        //Seed Data
        public IList<Physician> PhysicianList = new List<Physician>()
        {
            new Physician() { PhysicianId=1, FirstName="John", MiddleName="Legaspi", LastName="Palmero", Specialization="Cardiologist",
                            Schedule = new List<Schedule>()
                            {
                                new Schedule() {ScheduleId=1, Day="Monday", Time = new List<Time>() { new Time { TimeId=1, StartTime = 9, EndTime=10}, new Time { TimeId = 2, StartTime = 10, EndTime = 11}, new Time { TimeId = 3, StartTime = 11, EndTime = 12}, } },
                                new Schedule() {ScheduleId=2, Day="Wednesday", Time = new List<Time>() { new Time { TimeId = 1, StartTime =9, EndTime=10}, new Time { TimeId = 2, StartTime = 13, EndTime = 14}, new Time { TimeId = 3, StartTime = 14, EndTime = 15}, } },
                                new Schedule() {ScheduleId=3, Day="Friday", Time = new List<Time>() { new Time { TimeId = 1, StartTime =9, EndTime=10}, new Time { TimeId = 2, StartTime = 16, EndTime = 17}, new Time { TimeId = 3, StartTime = 17, EndTime = 18}, } },
                            }
            },
            new Physician() { PhysicianId=2, FirstName="Steve", MiddleName="Buendia", LastName="Gonzaga", Specialization="Dermatologist",
                            Schedule = new List<Schedule>()
                            {
                                new Schedule() {ScheduleId=1, Day="Tuesday", Time = new List<Time>() { new Time { TimeId = 1, StartTime =8, EndTime=9}, new Time { TimeId = 2, StartTime = 9, EndTime = 10}, new Time { TimeId = 3, StartTime = 10, EndTime = 11}, } },
                                new Schedule() {ScheduleId=2, Day="Thursday", Time = new List<Time>() { new Time { TimeId = 1, StartTime =9, EndTime=10}, new Time { TimeId = 2, StartTime = 10, EndTime = 11}, new Time { TimeId = 3, StartTime = 11, EndTime = 12}, } },
                                new Schedule() {ScheduleId=3, Day="Saturday", Time = new List<Time>() { new Time { TimeId = 1, StartTime =14, EndTime=15}, new Time { TimeId = 2, StartTime = 15, EndTime = 16}, new Time { TimeId = 3, StartTime = 16, EndTime = 17}, } },
                            }
            },
            new Physician() { PhysicianId=3, FirstName="Bill", MiddleName="Ogayre", LastName="Wyt", Specialization="Gastroenterologist",
                            Schedule = new List<Schedule>()
                            {
                                new Schedule() {ScheduleId=1, Day="Saturday", Time = new List<Time>() { new Time { TimeId = 1, StartTime =13, EndTime=14}, new Time { TimeId = 2, StartTime = 14, EndTime = 15}, new Time { TimeId = 3, StartTime = 15, EndTime = 16}, } },
                                new Schedule() {ScheduleId=2, Day="Sunday", Time = new List<Time>() { new Time { TimeId = 1, StartTime =11, EndTime=12}, new Time { TimeId = 2, StartTime = 13, EndTime = 14}, new Time { TimeId = 3, StartTime = 14, EndTime = 15}, } },
                                new Schedule() {ScheduleId=3, Day="Monday", Time = new List<Time>() { new Time { TimeId = 1, StartTime =10, EndTime=11}, new Time { TimeId = 2, StartTime = 11, EndTime = 12}, new Time { TimeId = 3, StartTime = 14, EndTime = 15}, } },
                            }
            },
            new Physician() { PhysicianId=4, FirstName="Ram", MiddleName="Batulan", LastName="Amor", Specialization="Hematologist/Oncologist",
                            Schedule = new List<Schedule>()
                            {
                                new Schedule() {ScheduleId=1, Day="Saturday", Time = new List<Time>() { new Time { TimeId = 1, StartTime =9, EndTime=10}, new Time { TimeId = 2, StartTime = 10, EndTime = 11}, new Time { TimeId = 3, StartTime = 11, EndTime = 12}, } },
                                new Schedule() {ScheduleId=2, Day="Sunday", Time = new List<Time>() { new Time { TimeId = 1, StartTime =13, EndTime=14}, new Time { TimeId = 2, StartTime = 14, EndTime = 15}, new Time { TimeId = 3, StartTime = 15, EndTime = 16}, } },
                                new Schedule() {ScheduleId=3, Day="Monday", Time = new List<Time>() { new Time { TimeId = 1, StartTime =14, EndTime=15}, new Time { TimeId = 2, StartTime = 15, EndTime = 16}, new Time { TimeId = 3, StartTime = 16, EndTime = 17}, } },
                            }
            },
            new Physician() { PhysicianId=5, FirstName="Ron", MiddleName="Dy", LastName="Martyr", Specialization="Neurologist",
                            Schedule = new List<Schedule>()
                            {
                                new Schedule() {ScheduleId=1, Day="Saturday", Time = new List<Time>() { new Time { TimeId = 1, StartTime =14, EndTime=15}, new Time { TimeId = 2, StartTime = 15, EndTime = 16}, new Time { TimeId = 3, StartTime = 16, EndTime = 17}, } },
                                new Schedule() {ScheduleId=2, Day="Sunday", Time = new List<Time>() { new Time { TimeId = 1, StartTime =11, EndTime=12}, new Time { TimeId = 2, StartTime = 13, EndTime = 14}, new Time { TimeId = 3, StartTime = 14, EndTime = 15}, } },
                                new Schedule() {ScheduleId=3, Day="Monday", Time = new List<Time>() { new Time { TimeId = 1, StartTime =13, EndTime=14}, new Time { TimeId = 2, StartTime = 14, EndTime = 15}, new Time { TimeId = 3, StartTime = 15, EndTime = 16}, } },
                            }
            }
        };

        // GET: Physician
        public ActionResult Index()
        {
            return View(PhysicianList);
        }
    }
}