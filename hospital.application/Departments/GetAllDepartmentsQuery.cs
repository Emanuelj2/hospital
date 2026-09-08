using hospital.application.DTOs;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Departments
{
    public record GetAllDepartmentsQuery : IRequest<List<DepartmentDto>>;

    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, List<DepartmentDto>>
    {
        private readonly IDepartmentRepository departmentRepository;

        public GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task<List<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await departmentRepository.GetAllAsync();
            return departments.Select(DepartmentMapper.ToDto).ToList();
        }
    }
}
