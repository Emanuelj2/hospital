using hospital.domain.Enums;
using hospital.domain.Organization.entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;

namespace hospital.domain.People
{
    public class Patient
    {
        public int Id { get; set; }


        //personal information
        public string FirstName { get; set; } = string.Empty;
        public string MiddelName { get; set; } = string.Empty;
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


        public int? UserAccountId { get; set; }
        public UserAccount? UserAccount { get; set; }

        public string MecicalRecordNumber { get; set; } = string.Empty;


        //admissin information
        public DateTime AdmissionDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public PatientStatus Status { get; set; } = PatientStatus.None;

        //assigned care
        public int? AssignedDoctorId { get; set; }
        public Employee? AssignedDoctor { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }


        //medical information
        public BloodType BloodType { get; set; } = BloodType.Unknown;
        public List<string> Allergies { get; set; } = new();
        public string? PrimaryDiagnosis { get; set; }


        //insurance information
        public string InsuranceProvider { get; set; } = string.Empty;
        public string InsurancePolicyNumber { get; set; } = string.Empty;


        public List<Visitor> Visitors { get; set; } = new();
        public string FullName => $"{FirstName} {MiddelName} {LastName}".Trim();

    }
}
