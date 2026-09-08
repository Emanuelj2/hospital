using hospital.application.DTOs;
using hospital.application.Exceptions;
using hospital.application.Interfaces;
using hospital.domain.Enums;
using hospital.domain.People;
using MediatR;

namespace hospital.application.Employees
{
    // Creates an Employee record together with the UserAccount (login) it's linked to.
    // This is the admin-only path for provisioning staff — public self-registration only
    // ever creates Patient accounts (see AuthController.Register).
    public record CreateEmployeeWithAccountCommand(EmployeeDto Employee, string Password, string Role) : IRequest<EmployeeDto>;

    public class CreateEmployeeWithAccountCommandHandler : IRequestHandler<CreateEmployeeWithAccountCommand, EmployeeDto>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IUserAccountRepository userAccountRepository;

        public CreateEmployeeWithAccountCommandHandler(IEmployeeRepository employeeRepository, IUserAccountRepository userAccountRepository)
        {
            this.employeeRepository = employeeRepository;
            this.userAccountRepository = userAccountRepository;
        }

        public async Task<EmployeeDto> Handle(CreateEmployeeWithAccountCommand request, CancellationToken cancellationToken)
        {
            if (!Enum.TryParse<AccountRole>(request.Role, ignoreCase: true, out var role)
                || role is AccountRole.None or AccountRole.Patient or AccountRole.Visitor)
            {
                throw new ValidationException($"'{request.Role}' is not a valid staff role.");
            }

            var existingAccount = await userAccountRepository.GetByEmailAsync(request.Employee.Email);
            if (existingAccount != null)
            {
                throw new ValidationException("Email is already registered.");
            }

            var account = new UserAccount
            {
                Email = request.Employee.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = role,
                IsActive = true
            };
            await userAccountRepository.AddAsync(account);

            var employee = EmployeeMapper.ToEntity(request.Employee);
            employee.UserAccountId = account.Id;
            await employeeRepository.AddAsync(employee);

            return EmployeeMapper.ToDto(employee);
        }
    }
}
