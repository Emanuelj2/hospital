using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.DTOs
{
    public class SeedResultDto
    {
        public bool Seeded { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
