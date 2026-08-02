using hospital.domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace hospital.application.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(30)]
        public string MiddleName { get; set; } = string.Empty;
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        [MaxLength(30)]
        public string Address { get; set; } = string.Empty;
        [MaxLength(30)]
        public string City { get; set; } = string.Empty;
        [MaxLength(30)]
        public string State { get; set; } = string.Empty;
        [MaxLength(5)]
        public string ZipCode { get; set; } = string.Empty;
        [MaxLength(30)]
        public string Country { get; set; } = string.Empty;

        [Required]
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
