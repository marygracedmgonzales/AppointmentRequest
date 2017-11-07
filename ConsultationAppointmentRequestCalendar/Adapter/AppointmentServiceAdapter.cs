using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConsultationAppointmentRequestCalendar.AppointmentServiceReference;
using ConsultationAppointmentRequestCalendar.Models;

namespace ConsultationAppointmentRequestCalendar.Adapter
{
    public class AppointmentServiceAdapter
    {
        AppointmentRequestClient client = new AppointmentRequestClient();
        
        public List<Physician> getPhysicians()
        {
            List<Physician> physicians = new List<Physician>();
            PhysicianEntity[] physiciansEntity = client.GetPhysicians();
            try
            {
                if (physiciansEntity.Count() != 0)
                {
                    foreach (PhysicianEntity p in physiciansEntity)
                    {
                        Physician physician = new Physician();
                        physician.PhysicianId = p.PhysicianId;
                        physician.FirstName = p.FirstName;
                        physician.MiddleName = p.MiddleName;
                        physician.LastName = p.LastName;
                        physician.Specialization = p.Specialization;
                        physician.Schedule = mapPhysicianSchedules(p.Schedule);

                        physicians.Add(physician);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return physicians;
        }

        private List<Schedule> mapPhysicianSchedules(ScheduleEntity[] physicianSchedules)
        {
            List<Schedule> schedules = new List<Schedule>();
            try
            {
                if(physicianSchedules.Count() != 0)
                {
                    foreach (ScheduleEntity s in physicianSchedules)
                    {
                        Schedule schedule = new Schedule();
                        schedule.ScheduleId = s.ScheduleId;
                        schedule.Day = s.Day;
                        schedule.Time = mapPhysicianTime(s.Time);

                        schedules.Add(schedule);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return schedules;
        }

        private List<Time> mapPhysicianTime(TimeEntity[] physicianTime)
        {
            List<Time> timeList = new List<Time>();
            try
            {
                if(physicianTime.Count() != 0)
                {
                    foreach (TimeEntity t in physicianTime)
                    {
                        Time time = new Time();
                        time.TimeId = t.TimeId;
                        time.StartTime = t.StartTime;
                        time.EndTime = t.EndTime;

                        timeList.Add(time);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return timeList;
        }

        public Physician getPhysician(int physicianId)
        {
            PhysicianEntity physiciansEntity = client.GetPhysician(physicianId);
            Physician physician = new Physician();

            if (physiciansEntity != null)
            {
                physician.PhysicianId = physiciansEntity.PhysicianId;
                physician.FirstName = physiciansEntity.FirstName;
                physician.MiddleName = physiciansEntity.MiddleName;
                physician.LastName = physiciansEntity.LastName;
                physician.Specialization = physiciansEntity.Specialization;
                physician.Schedule = mapPhysicianSchedules(physiciansEntity.Schedule);
            }
            return physician;
        }

        public List<Appointment> getAppointmentList()
        {
            List<Appointment> appointments = new List<Appointment>();
            AppointmentEntity[] appointmentEntity = client.GetAppointmentList();
            try
            {
                if(appointmentEntity.Count() != 0)
                {
                    foreach (AppointmentEntity a in appointmentEntity)
                    {
                        Appointment appointment = new Appointment();
                        appointment.AppointmentId = a.AppointmentId;
                        appointment.PatientId = a.PatientId;
                        appointment.PhysicianId = a.PhysicianId;
                        appointment.ScheduleId = a.ScheduleId;
                        appointment.Date = a.Date;
                        appointment.TimeId = a.TimeId;
                        appointment.Purpose = a.PurposeId;
                        appointment.Remarks = a.Remarks;
                        appointments.Add(appointment);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appointments;
        }

        public Appointment setAppointment(int physicianId)
        {
            Appointment appointment = new Appointment();
            try
            {
                AppointmentEntity appointmentEntity = client.SetAppointment(physicianId);

                if (appointmentEntity != null)
                {
                    appointment.AppointmentId = appointmentEntity.AppointmentId;
                    appointment.PatientId = appointmentEntity.PatientId;
                    appointment.PhysicianId = appointmentEntity.PhysicianId;
                    appointment.ScheduleId = appointmentEntity.ScheduleId;
                    appointment.Date = appointmentEntity.Date;
                    appointment.TimeId = appointmentEntity.TimeId;
                    appointment.Purpose = appointmentEntity.PurposeId;
                    appointment.Remarks = appointmentEntity.Remarks;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appointment;
        }

        public List<Patient> getPatientList()
        {
            List<Patient> patients = new List<Patient>();
            PatientEntity[] patientsEntity = client.GetPatientList();
            try
            {
                if (patientsEntity.Count() != 0)
                {
                    foreach (PatientEntity p in patientsEntity)
                    {
                        Patient patient = new Patient();
                        patient.PatientId = p.PatientId;
                        patient.FirstName = p.FirstName;
                        patient.MiddleName = p.MiddleName;
                        patient.LastName = p.LastName;
                        patient.Age = p.Age;

                        patients.Add(patient);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return patients;
        }

        public bool saveAppointment(Appointment AppointmentRequest)
        {
            bool isSaved = false;
            AppointmentEntity AppointmentRequestParameter = new AppointmentEntity();
            AppointmentRequestParameter.PatientId = AppointmentRequest.PatientId;
            AppointmentRequestParameter.PhysicianId = AppointmentRequest.PhysicianId;
            AppointmentRequestParameter.ScheduleId = AppointmentRequest.ScheduleId;
            AppointmentRequestParameter.Date = AppointmentRequest.Date;
            AppointmentRequestParameter.TimeId = AppointmentRequest.TimeId;
            AppointmentRequestParameter.PurposeId = AppointmentRequest.Purpose;
            AppointmentRequestParameter.Remarks = AppointmentRequest.Remarks;

            isSaved = client.SaveAppointment(AppointmentRequestParameter);

            return isSaved;
        }

        public bool updateAppointment(Appointment UpdatedRequest)
        {
            bool isUpdated = false;
            AppointmentEntity AppointmentRequestParameter = new AppointmentEntity();
            AppointmentRequestParameter.AppointmentId = UpdatedRequest.AppointmentId;
            AppointmentRequestParameter.PatientId = UpdatedRequest.PatientId;
            AppointmentRequestParameter.PhysicianId = UpdatedRequest.PhysicianId;
            AppointmentRequestParameter.ScheduleId = UpdatedRequest.ScheduleId;
            AppointmentRequestParameter.Date = UpdatedRequest.Date;
            AppointmentRequestParameter.TimeId = UpdatedRequest.TimeId;
            AppointmentRequestParameter.PurposeId = UpdatedRequest.Purpose;
            AppointmentRequestParameter.Remarks = UpdatedRequest.Remarks;

            isUpdated = client.UpdateAppointment(AppointmentRequestParameter);

            return isUpdated;
        }

        public bool deleteAppointment(int appointmentId)
        {
            bool isDeleted = false;
            isDeleted = client.DeleteAppointment(appointmentId);
            return isDeleted;
        }
    }
}