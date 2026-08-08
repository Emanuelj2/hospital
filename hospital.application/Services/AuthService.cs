using hospital.application.DTOs;
using hospital.application.Interfaces;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens.Experimental;
using hospital.domain.People;

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

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var account = await userAccountRepository.GetByEmailAsync(registerDto.Email);

            if(account != null)
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            var newAccount = new UserAccount
            {
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = registerDto.Role,
                IsActive = true
            };

            await userAccountRepository.AddAsync(newAccount);

            var (token, expiresAt) = tokenService.GenerateToken(newAccount);

            return new AuthResponseDto
            {
                UserAccountId = newAccount.Id,
                Email = newAccount.Email,
                Role = newAccount.Role,
                Token = token,
                ExpiresAt = expiresAt
            };
        }
    }
}
