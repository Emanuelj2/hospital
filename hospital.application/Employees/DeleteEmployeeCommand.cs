using hospital.application.Exceptions;
using hospital.application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hospital.application.Employees
{
    public record DeleteEmployeeCommand(int Id) : IRequest;

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository employeeRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.GetByIdAsync(request.Id);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with ID {request.Id} not found.");
            }

            try
            {
                await employeeRepository.DeleteAsync(request.Id);
            }
            // DbUpdateException: the database rejects the DELETE outright (a real FK violation) —
            // e.g. this employee is still a Department's Head, or has appointments/prescriptions/
            // lab results referencing them. InvalidOperationException: EF's change tracker catches
            // an equivalent conflict client-side first. Either way, something still points at them.
            catch (Exception ex) when (ex is DbUpdateException or InvalidOperationException)
            {
                throw new ValidationException("Cannot delete this employee while they are still referenced elsewhere (e.g. as a department head, or on appointments/prescriptions/lab results). Reassign those first.");
            }
        }
    }
}
