using hospital.application.DTOs;
using hospital.application.Exceptions;
using hospital.application.Interfaces;
using BCrypt.Net;
using hospital.domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using hospital.domain.People;

namespace hospital.application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserAccountRepository userAccountRepository;
        private readonly ITokenService tokenService;

        // Used to run a BCrypt comparison even when no account was found, so a login
        // attempt against an unknown email takes the same time as one against a known
        // email with a wrong password — otherwise response timing leaks which emails exist.
        private static readonly string DummyPasswordHash = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString());

        public AuthService(IUserAccountRepository userAccountRepository, ITokenService tokenService)
        {
            this.userAccountRepository = userAccountRepository;
            this.tokenService = tokenService;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var account = await userAccountRepository.GetByEmailAsync(loginDto.Email);

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, account?.PasswordHash ?? DummyPasswordHash);

            if (account == null || !account.IsActive || !isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var (token, expiresAt) = tokenService.GenerateToken(account);

            return new AuthResponseDto
            {
                UserAccountId = account.Id,
                Email = account.Email,
                Role = account.Role.ToString(),
                Token = token,
                ExpiresAt = expiresAt
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            if (!Enum.TryParse<AccountRole>(registerDto.Role, ignoreCase: true, out var role) || role == AccountRole.None)
            {
                throw new ValidationException($"'{registerDto.Role}' is not a valid role.");
            }

            var account = await userAccountRepository.GetByEmailAsync(registerDto.Email);

            if(account != null)
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            var newAccount = new UserAccount
            {
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = role,
                IsActive = true
            };

            await userAccountRepository.AddAsync(newAccount);

            var (token, expiresAt) = tokenService.GenerateToken(newAccount);

            return new AuthResponseDto
            {
                UserAccountId = newAccount.Id,
                Email = newAccount.Email,
                Role = newAccount.Role.ToString(),
                Token = token,
                ExpiresAt = expiresAt
            };
        }
    }
}
