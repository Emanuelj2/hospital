using hospital.application.DTOs;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Auth
{
    public record LoginCommand(LoginDto Login) : IRequest<AuthResponseDto>;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly IUserAccountRepository userAccountRepository;
        private readonly ITokenService tokenService;

        // Run a BCrypt comparison even when no account was found, so a login attempt
        // against an unknown email takes the same time as one against a known email
        // with a wrong password — otherwise response timing leaks which emails exist.
        private static readonly string DummyPasswordHash = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString());

        public LoginCommandHandler(IUserAccountRepository userAccountRepository, ITokenService tokenService)
        {
            this.userAccountRepository = userAccountRepository;
            this.tokenService = tokenService;
        }

        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var account = await userAccountRepository.GetByEmailAsync(request.Login.Email);

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Login.Password, account?.PasswordHash ?? DummyPasswordHash);

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
    }
}
