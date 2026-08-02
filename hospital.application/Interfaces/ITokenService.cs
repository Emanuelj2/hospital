using hospital.domain.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.Interfaces
{
    public interface ITokenService
    {
        (string Token, System.DateTime ExpiresAt) GenerateToken(UserAccount account);
    }
}
