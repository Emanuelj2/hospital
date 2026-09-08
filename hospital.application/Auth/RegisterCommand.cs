using hospital.application.DTOs;
using hospital.application.Exceptions;
using hospital.application.Interfaces;
using hospital.domain.Enums;
using hospital.domain.People;
using MediatR;

namespace hospital.application.Auth
{
    public record RegisterCommand(RegisterDto Registration) : IRequest<AuthResponseDto>;

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
    {
        private readonly IUserAccountRepository userAccountRepository;
        private readonly ITokenService tokenService;

        public RegisterCommandHandler(IUserAccountRepository userAccountRepository, ITokenService tokenService)
        {
            this.userAccountRepository = userAccountRepository;
            this.tokenService = tokenService;
        }

        public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<AccountRole>(request.Registration.Role, ignoreCase: true, out var role) || role == AccountRole.None)
            {
                throw new ValidationException($"'{request.Registration.Role}' is not a valid role.");
            }

            var account = await userAccountRepository.GetByEmailAsync(request.Registration.Email);
            if (account != null)
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            var newAccount = new UserAccount
            {
                Email = request.Registration.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Registration.Password),
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
