using hospital.application.Exceptions;
using hospital.application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hospital.application.Departments
{
    public record DeleteDepartmentCommand(int Id) : IRequest;

    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
    {
        private readonly IDepartmentRepository departmentRepository;

        public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await departmentRepository.GetByIdAsync(request.Id);
            if (department == null)
            {
                throw new NotFoundException($"Department with ID {request.Id} not found.");
            }

            try
            {
                await departmentRepository.DeleteAsync(request.Id);
            }
            // DbUpdateException: the database rejects the DELETE outright (a real FK violation).
            // InvalidOperationException: EF's change tracker catches it first, client-side —
            // happens when a referencing Employee (e.g. this department's own Head) is already
            // tracked in the same context, since its required, non-nullable DepartmentId FK
            // would otherwise be left dangling. Both mean the same thing: employees/patients
            // still reference this department.
            catch (Exception ex) when (ex is DbUpdateException or InvalidOperationException)
            {
                throw new ValidationException("Cannot delete a department that still has employees or patients assigned to it.");
            }
        }
    }
}
