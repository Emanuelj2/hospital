using hospital.application.DTOs;
using hospital.application.Exceptions;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Departments
{
    public record UpdateDepartmentCommand(int Id, DepartmentDto Department) : IRequest;

    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
    {
        private readonly IDepartmentRepository departmentRepository;

        public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await departmentRepository.GetByIdAsync(request.Id);
            if (department == null)
            {
                throw new NotFoundException($"Department with ID {request.Id} not found.");
            }

            DepartmentMapper.ApplyTo(department, request.Department);
            await departmentRepository.UpdateAsync(department);
        }
    }
}
