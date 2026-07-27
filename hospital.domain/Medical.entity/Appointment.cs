using hospital.domain.Enums;
using hospital.domain.Organization.entity;
using hospital.domain.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.domain.Medical.entity
{
    public class Appointment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int DoctorId { get; set; }
        public Employee? Doctor { get; set; }
        
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public DateTime ScheduledDateTime { get; set; }
        public int DurationInMinutes { get; set; } = 30;

        public string Reason { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    }
}
