using hospital.application.Interfaces;
using hospital.domain.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.Services
{
    public class TokenService : ITokenService
    {
        
        public (string Token, DateTime ExpiresAt) GenerateToken(UserAccount account)
        {
            throw new NotImplementedException();
        }
    }
}
