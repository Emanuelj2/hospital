using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.DTOs
{
    // Wraps an Employee record together with the login the admin is provisioning for them.
    public class CreateStaffAccountDto
    {
        public EmployeeDto Employee { get; set; } = new();
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // "Employee", "Doctor", or "Admin"
    }
}
