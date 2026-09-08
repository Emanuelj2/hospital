using hospital.application.DTOs;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Employees
{
    public record GetEmployeeByPhoneNumberQuery(string PhoneNumber) : IRequest<EmployeeDto?>;

    public class GetEmployeeByPhoneNumberQueryHandler : IRequestHandler<GetEmployeeByPhoneNumberQuery, EmployeeDto?>
    {
        private readonly IEmployeeRepository employeeRepository;

        public GetEmployeeByPhoneNumberQueryHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto?> Handle(GetEmployeeByPhoneNumberQuery request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetByPhoneNumberAsync(request.PhoneNumber);
            return employee != null ? EmployeeMapper.ToDto(employee) : null;
        }
    }
}
