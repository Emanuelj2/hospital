using hospital.application.DTOs;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Employees
{
    public record CreateEmployeeCommand(EmployeeDto Employee) : IRequest<EmployeeDto>;

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
    {
        private readonly IEmployeeRepository employeeRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = EmployeeMapper.ToEntity(request.Employee);
            await employeeRepository.AddAsync(employee);

            request.Employee.Id = employee.Id;
            return request.Employee;
        }
    }
}
