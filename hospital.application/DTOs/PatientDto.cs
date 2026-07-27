using hospital.domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public string MedicalRecordNumber { get; set; } = string.Empty;

        public DateTime AdmissionDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public PatientStatus Status { get; set; } = PatientStatus.None;

        public int? AssignedDoctorId { get; set; }
        public int? DepartmentId { get; set; }
        public string? RoomNumber { get; set; }

        public BloodType BloodType { get; set; } = BloodType.Unknown;
        public List<string> Allergies { get; set; } = new();
        public string? PrimaryDiagnosis { get; set; }

        public string InsuranceProvider { get; set; } = string.Empty;
        public string InsurancePolicyNumber { get; set; } = string.Empty;
    }
}
