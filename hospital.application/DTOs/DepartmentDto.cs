using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int? HeadEmployeeId { get; set; }
        public string? HeadEmployeeName { get; set; }
    }
}
