using hospital.application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.Interfaces
{
    public interface IAuthService
    {
        // Throws UnauthorizedException if the email/password is invalid
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    }
}
