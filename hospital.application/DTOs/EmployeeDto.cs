using hospital.domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        // Personal information
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

        // Link to login/account record
        public int? UserAccountId { get; set; }

        public string StateLicenceNumber { get; set; } = string.Empty;

        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }

        public JobTitle Job { get; set; } = JobTitle.None;
        public EmployeeType EmploymentType { get; set; } = EmployeeType.FullTime;
        public AccessLevelType AccessLevel { get; set; } = AccessLevelType.Basic;

        // Department relationship
        public int DepartmentId { get; set; }

        // Optional
        public string Race { get; set; } = string.Empty;
        public string Ethnicity { get; set; } = string.Empty;
        public bool VeteranStatus { get; set; } = false;
        public Pronouns Pronoun { get; set; } = Pronouns.None;
    }
}
