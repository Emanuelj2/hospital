using hospital.application.DTOs;
using hospital.application.Interfaces;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserAccountRepository userAccountRepository;
        private readonly ITokenService tokenService;

        public AuthService(IUserAccountRepository userAccountRepository, ITokenService tokenService)
        {
            this.userAccountRepository = userAccountRepository;
            this.tokenService = tokenService;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var account = await userAccountRepository.GetByEmailAsync(loginDto.Email);

            if(account == null || !account.IsActive)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, account.PasswordHash);
            if(!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var (token, expiresAt) = tokenService.GenerateToken(account);

            return new AuthResponseDto
            {
                UserAccountId = account.Id,
                Email = account.Email,
                Role = account.Role,
                Token = token,
                ExpiresAt = expiresAt
            };
        }
    }
}
