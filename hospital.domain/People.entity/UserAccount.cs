using hospital.domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.domain.People
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public AccountRole Role { get; set; } = AccountRole.None;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
