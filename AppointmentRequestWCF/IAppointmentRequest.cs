using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AppointmentRequestWCF
{
    [ServiceContract]
    public interface IAppointmentRequest
    {        
        [OperationContract]
        List<PhysicianEntity> GetPhysicians();

        [OperationContract]
        PhysicianEntity GetPhysician(int physicianId);

        [OperationContract]
        List<AppointmentEntity> GetAppointmentList();

        [OperationContract]
        AppointmentEntity SetAppointment(int physicianId);

        [OperationContract]
        List<PatientEntity> GetPatientList();

        [OperationContract]
        bool SaveAppointment(AppointmentEntity appointmentRequest);

        [OperationContract]
        bool UpdateAppointment(AppointmentEntity updatedRequest);

        [OperationContract]
        bool DeleteAppointment(int appointmentId);
    }

   [DataContract]
    public class PhysicianEntity
    {
        [DataMember]
        public int PhysicianId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Specialization { get; set; }
        [DataMember]
        public List<ScheduleEntity> Schedule { get; set; }
    }

    [DataContract]
    public class ScheduleEntity
    {
        [DataMember]
        public int ScheduleId { get; set; }
        [DataMember]
        public string Day { get; set; }
        [DataMember]
        public List<TimeEntity> Time { get; set; }
    }

    [DataContract]
    public class TimeEntity
    {
        [DataMember]
        public int TimeId { get; set; }
        [DataMember]
        public int StartTime { get; set; }
        [DataMember]
        public int EndTime { get; set; }
    }

    [DataContract]
    public class PatientEntity
    {
        [DataMember]
        public int PatientId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string MiddleName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public int Age { get; set; }
    }

    [DataContract]
    public class AppointmentEntity
    {
        [DataMember]
        public int AppointmentId { get; set; }
        [DataMember]
        public int PatientId { get; set; }
        [DataMember]
        public int PhysicianId { get; set; }
        [DataMember]
        public int ScheduleId { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public int TimeId { get; set; }
        [DataMember]
        public int PurposeId { get; set; }
        [DataMember]
        public string Remarks { get; set; }
    }
}
