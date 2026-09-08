using hospital.application.DTOs;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Employees
{
    public record GetEmployeesByFirstNameQuery(string FirstName) : IRequest<List<EmployeeDto>>;

    public class GetEmployeesByFirstNameQueryHandler : IRequestHandler<GetEmployeesByFirstNameQuery, List<EmployeeDto>>
    {
        private readonly IEmployeeRepository employeeRepository;

        public GetEmployeesByFirstNameQueryHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDto>> Handle(GetEmployeesByFirstNameQuery request, CancellationToken cancellationToken)
        {
            var employees = await employeeRepository.GetByFirstNameAsync(request.FirstName);
            return employees.Select(EmployeeMapper.ToDto).ToList();
        }
    }
}
