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

        //creates a new user account with a hashed password and returns the created account's details
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    }
}
