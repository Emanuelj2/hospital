using hospital.application.DTOs;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Employees
{
    public record GetEmployeesByLastNameQuery(string LastName) : IRequest<List<EmployeeDto>>;

    public class GetEmployeesByLastNameQueryHandler : IRequestHandler<GetEmployeesByLastNameQuery, List<EmployeeDto>>
    {
        private readonly IEmployeeRepository employeeRepository;

        public GetEmployeesByLastNameQueryHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDto>> Handle(GetEmployeesByLastNameQuery request, CancellationToken cancellationToken)
        {
            var employees = await employeeRepository.GetByLastNameAsync(request.LastName);
            return employees.Select(EmployeeMapper.ToDto).ToList();
        }
    }
}
