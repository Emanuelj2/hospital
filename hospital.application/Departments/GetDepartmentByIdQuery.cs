using hospital.application.DTOs;
using hospital.application.Exceptions;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Departments
{
    public record GetDepartmentByIdQuery(int Id) : IRequest<DepartmentDto>;

    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto>
    {
        private readonly IDepartmentRepository departmentRepository;

        public GetDepartmentByIdQueryHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task<DepartmentDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await departmentRepository.GetByIdAsync(request.Id);
            if (department == null)
            {
                throw new NotFoundException($"Department with ID {request.Id} not found.");
            }

            return DepartmentMapper.ToDto(department);
        }
    }
}
