using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AppointmentRequestWCF
{
    public class AppointmentRequest : IAppointmentRequest
    {
        AppointmentRequestContext appointmentRequestContext = new AppointmentRequestContext();

        public List<PhysicianEntity> GetPhysicians()
        {
            List<PhysicianEntity> physicians = new List<PhysicianEntity>();
            try
            {
               List<physician> physiciansDb = appointmentRequestContext.physicians.ToList();

                if (physiciansDb.Count() != 0)
                {
                    foreach (physician p in physiciansDb)
                    {
                        PhysicianEntity physician = new PhysicianEntity();
                        physician.PhysicianId = p.physician_id;
                        physician.FirstName = p.first_name;
                        physician.MiddleName = p.middle_name;
                        physician.LastName = p.last_name;
                        physician.Specialization = p.specialization;
                        physician.Schedule = GetPhysicianSchedules(p.physician_id);
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

        private List<ScheduleEntity> GetPhysicianSchedules(int physicianId)
        {
            List<ScheduleEntity> physicianSchedulesEntity = new List<ScheduleEntity>();
            try
            {
               
                var query = from stm in appointmentRequestContext.schedule_time_mapping
                            join s in appointmentRequestContext.schedules on stm.schedule_id equals s.schedule_id
                            where stm.physician_id == physicianId
                            select s;
                List<schedule> physicianSchedulesDB = query.Distinct().ToList();

                if (physicianSchedulesDB.Count() != 0)
                {
                    foreach (schedule s in physicianSchedulesDB)
                    {
                        ScheduleEntity schedule = new ScheduleEntity();
                        schedule.ScheduleId = s.schedule_id;
                        schedule.Day = s.day;
                        schedule.Time = GetPhysicianTime(s.schedule_id);
                        physicianSchedulesEntity.Add(schedule);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return physicianSchedulesEntity;
        }

        private List<TimeEntity> GetPhysicianTime(int scheduleId)
        {
            List<TimeEntity> physicianTimeEntity = new List<TimeEntity>();
            try
            {

                var query = from stm in appointmentRequestContext.schedule_time_mapping
                            join t in appointmentRequestContext.times on stm.time_id equals t.time_id
                            where stm.schedule_id == scheduleId
                            select t;
                List<time> physicianTimeDB = query.Distinct().ToList();

                if (physicianTimeDB.Count() != 0)
                {
                    foreach (time t in physicianTimeDB)
                    {
                        TimeEntity time = new TimeEntity();
                        time.TimeId = t.time_id;
                        time.StartTime = t.start_time;
                        time.EndTime = t.end_time;
                        physicianTimeEntity.Add(time);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return physicianTimeEntity;
        }

        public PhysicianEntity GetPhysician(int physicianId)
        {
            PhysicianEntity physician = new PhysicianEntity();

            try
            {
                physician physicianDb = appointmentRequestContext.physicians.Where(p => p.physician_id == physicianId).FirstOrDefault();

                if (physicianDb != null)
                {
                    physician.PhysicianId = physicianDb.physician_id;
                    physician.FirstName = physicianDb.first_name;
                    physician.MiddleName = physicianDb.middle_name;
                    physician.LastName = physicianDb.last_name;
                    physician.Specialization = physicianDb.specialization;
                    physician.Schedule = GetPhysicianSchedules(physicianId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return physician;
        }

        public List<AppointmentEntity> GetAppointmentList()
        {
            List<AppointmentEntity> appointments = new List<AppointmentEntity>();
            try
            {
                List<appointment> appointmentsDB = appointmentRequestContext.appointments.ToList();

                if (appointmentsDB.Count() != 0)
                {
                    foreach (appointment a in appointmentsDB)
                    {
                        AppointmentEntity appointment = new AppointmentEntity();
                        appointment.AppointmentId = a.appointment_id;
                        appointment.PatientId = Convert.ToInt32(a.patient_id);
                        appointment.PhysicianId = Convert.ToInt32(a.physician_id);
                        appointment.ScheduleId = Convert.ToInt32(a.schedule_id);
                        appointment.Date = Convert.ToDateTime(a.date_time);
                        appointment.TimeId = Convert.ToInt32(a.time_id);
                        appointment.PurposeId = Convert.ToInt32(a.purpose_id);
                        appointment.Remarks = a.remarks;
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

        public AppointmentEntity SetAppointment(int physicianId)
        {
            AppointmentEntity appointment = new AppointmentEntity();
            try
            {
                appointment appointmentDB = appointmentRequestContext.appointments.Where(a => a.physician_id == physicianId).FirstOrDefault();

                if (appointmentDB != null)
                {
                    appointment.AppointmentId = appointmentDB.appointment_id;
                    appointment.PatientId = Convert.ToInt32(appointmentDB.patient_id);
                    appointment.PhysicianId = Convert.ToInt32(appointmentDB.physician_id);
                    appointment.ScheduleId = Convert.ToInt32(appointmentDB.schedule_id);
                    appointment.Date = Convert.ToDateTime(appointmentDB.date_time);
                    appointment.TimeId = Convert.ToInt32(appointmentDB.time_id);
                    appointment.PurposeId = Convert.ToInt32(appointmentDB.purpose_id);
                    appointment.Remarks = appointmentDB.remarks;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return appointment;
        }

        public List<PatientEntity> GetPatientList()
        {
            List<PatientEntity> patients = new List<PatientEntity>();
            try
            {
                List<patient> patientsDb = appointmentRequestContext.patients.ToList();

                if (patientsDb.Count() != 0)
                {
                    foreach (patient p in patientsDb)
                    {
                        PatientEntity patient = new PatientEntity();
                        patient.PatientId = p.patient_id;
                        patient.FirstName = p.first_name;
                        patient.MiddleName = p.middle_name;
                        patient.LastName = p.last_name;
                        patient.Age = Convert.ToInt32(p.age);
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

        public bool SaveAppointment(AppointmentEntity appointmentRequest)
        {
            bool isSaved = false;
            appointment appointmentRequestParameter = new appointment();
                       
            try
            {
                appointmentRequestParameter.patient_id = appointmentRequest.PatientId;
                appointmentRequestParameter.physician_id = appointmentRequest.PhysicianId;
                appointmentRequestParameter.schedule_id = appointmentRequest.ScheduleId;
                appointmentRequestParameter.date_time = appointmentRequest.Date;
                appointmentRequestParameter.time_id = appointmentRequest.TimeId;
                appointmentRequestParameter.purpose_id = appointmentRequest.PurposeId;
                appointmentRequestParameter.remarks = appointmentRequest.Remarks;

                List<appointment> appointmentsDb = appointmentRequestContext.appointments.ToList();
                appointmentRequestContext.appointments.Add(appointmentRequestParameter);
                appointmentRequestContext.SaveChanges();
                List<appointment> appointmentsDbUpdated = appointmentRequestContext.appointments.ToList();
               
                if (appointmentsDb.Count() < appointmentsDbUpdated.Count())
                { isSaved = true; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSaved;
        }

        public bool UpdateAppointment(AppointmentEntity updatedRequest)
        {
            bool isUpdated = false;
            int result = 0;
            try
            {
                appointment appointmentRequestUpdated = appointmentRequestContext.appointments.Single(a => a.appointment_id == updatedRequest.AppointmentId);

                appointmentRequestUpdated.appointment_id = updatedRequest.AppointmentId;
                appointmentRequestUpdated.patient_id = updatedRequest.PatientId;
                appointmentRequestUpdated.physician_id = updatedRequest.PhysicianId;
                appointmentRequestUpdated.schedule_id = updatedRequest.ScheduleId;
                appointmentRequestUpdated.date_time = updatedRequest.Date;
                appointmentRequestUpdated.time_id = updatedRequest.TimeId;
                appointmentRequestUpdated.purpose_id = updatedRequest.PurposeId;
                appointmentRequestUpdated.remarks = updatedRequest.Remarks;
                result = appointmentRequestContext.SaveChanges();

                if (result != 0)
                { isUpdated = true; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isUpdated;
        }

        public bool DeleteAppointment(int appointmentId)
        {
            bool isDeleted = false;
            int result = 0;
            try
            {
                appointment appointmentRequestUpdated = appointmentRequestContext.appointments.Single(a => a.appointment_id == appointmentId);
                appointmentRequestContext.appointments.Remove(appointmentRequestUpdated);
                result = appointmentRequestContext.SaveChanges();

                if (result != 0)
                { isDeleted = true; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isDeleted;
        }
    }

}
