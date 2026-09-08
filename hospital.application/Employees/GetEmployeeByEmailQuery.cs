using hospital.application.DTOs;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Employees
{
    public record GetEmployeeByEmailQuery(string Email) : IRequest<EmployeeDto?>;

    public class GetEmployeeByEmailQueryHandler : IRequestHandler<GetEmployeeByEmailQuery, EmployeeDto?>
    {
        private readonly IEmployeeRepository employeeRepository;

        public GetEmployeeByEmailQueryHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto?> Handle(GetEmployeeByEmailQuery request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetByEmailAsync(request.Email);
            return employee != null ? EmployeeMapper.ToDto(employee) : null;
        }
    }
}
