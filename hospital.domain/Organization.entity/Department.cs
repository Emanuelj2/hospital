using hospital.domain.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.domain.Organization.entity
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public int? HeadEmployeeId { get; set; } 
        public Employee? Head { get; set; } 

        public List<Employee> Employees { get; set; } = new(); 
        public List<Patient> Patients { get; set; } = new();
    }
}
