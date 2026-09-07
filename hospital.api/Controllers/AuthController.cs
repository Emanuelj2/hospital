using hospital.application.DTOs;
using hospital.application.Exceptions;
using hospital.application.Interfaces;
using hospital.domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System;

namespace hospital.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        [EnableRateLimiting("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            try
            {
                var result = await authService.LoginAsync(loginDto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        // Public self-registration is only allowed for the Patient role. Employee/Doctor/Admin
        // accounts carry elevated access and must be created by an already-authenticated Admin
        // (or, for local dev, via the Seed Data button) — never by an anonymous caller choosing
        // their own role.
        [HttpPost("Register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
        {
            var isSelfRegisteringAsPatient = string.Equals(registerDto.Role, nameof(AccountRole.Patient), StringComparison.OrdinalIgnoreCase);
            var callerIsAdmin = User.Identity?.IsAuthenticated == true && User.IsInRole(nameof(AccountRole.Admin));

            if (!isSelfRegisteringAsPatient && !callerIsAdmin)
            {
                return Forbid();
            }

            try
            {
                var result = await authService.RegisterAsync(registerDto);
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
