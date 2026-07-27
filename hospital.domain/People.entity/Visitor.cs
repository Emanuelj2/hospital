using hospital.domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.domain.People
{
    public class Visitor
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public RelationshipType Relationship { get; set; } = RelationshipType.None;

        
        public List<Patient> VisitingPatients { get; set; } = new();

        
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }

        
        public bool IsEmergencyContact { get; set; } = false;

        public string FullName() => $"{FirstName} {LastName}";
    }
}
