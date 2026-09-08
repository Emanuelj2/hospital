using hospital.application.DTOs;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Departments
{
    public record CreateDepartmentCommand(DepartmentDto Department) : IRequest<DepartmentDto>;

    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
    {
        private readonly IDepartmentRepository departmentRepository;

        public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = DepartmentMapper.ToEntity(request.Department);
            await departmentRepository.AddAsync(department);

            request.Department.Id = department.Id;
            return request.Department;
        }
    }
}
