using hospital.application.Exceptions;
using hospital.application.Interfaces;
using hospital.domain.Enums;
using MediatR;

namespace hospital.application.Employees
{
    // Changes the role on an employee's existing login. This is intentionally separate from
    // UpdateEmployeeCommand: role is a property of the linked UserAccount, not the Employee
    // record, and changing it has security implications the plain field-edit path shouldn't
    // casually carry.
    public record UpdateEmployeeRoleCommand(int EmployeeId, string Role) : IRequest;

    public class UpdateEmployeeRoleCommandHandler : IRequestHandler<UpdateEmployeeRoleCommand>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IUserAccountRepository userAccountRepository;

        public UpdateEmployeeRoleCommandHandler(IEmployeeRepository employeeRepository, IUserAccountRepository userAccountRepository)
        {
            this.employeeRepository = employeeRepository;
            this.userAccountRepository = userAccountRepository;
        }

        public async Task Handle(UpdateEmployeeRoleCommand request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with ID {request.EmployeeId} not found.");
            }

            if (employee.UserAccountId == null)
            {
                throw new ValidationException("This employee has no login account to update.");
            }

            if (!Enum.TryParse<AccountRole>(request.Role, ignoreCase: true, out var role)
                || role is AccountRole.None or AccountRole.Patient or AccountRole.Visitor)
            {
                throw new ValidationException($"'{request.Role}' is not a valid staff role.");
            }

            var account = await userAccountRepository.GetByIdAsync(employee.UserAccountId.Value);
            if (account == null)
            {
                throw new NotFoundException("Linked login account not found.");
            }

            account.Role = role;
            await userAccountRepository.UpdateAsync(account);
        }
    }
}
