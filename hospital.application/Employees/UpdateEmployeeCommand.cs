using hospital.application.DTOs;
using hospital.application.Exceptions;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Employees
{
    public record UpdateEmployeeCommand(int Id, EmployeeDto Employee) : IRequest;

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository employeeRepository;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetByIdAsync(request.Id);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with ID {request.Id} not found.");
            }

            EmployeeMapper.ApplyTo(employee, request.Employee);
            await employeeRepository.UpdateAsync(employee);
        }
    }
}
