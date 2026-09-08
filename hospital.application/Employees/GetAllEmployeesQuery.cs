using hospital.application.DTOs;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Employees
{
    public record GetAllEmployeesQuery : IRequest<List<EmployeeDto>>;

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IEmployeeRepository employeeRepository;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await employeeRepository.GetAllAsync();
            return employees.Select(EmployeeMapper.ToDto).ToList();
        }
    }
}
