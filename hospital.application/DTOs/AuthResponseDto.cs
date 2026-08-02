using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.DTOs
{
    public class AuthResponseDto
    {
        public int UserAccountId { get; set; }
        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty; // "Employee", "Patient", "Visitor"
        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

    }
}
