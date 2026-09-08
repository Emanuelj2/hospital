using hospital.application.Exceptions;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Patients
{
    public record DeletePatientCommand(int Id) : IRequest;

    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand>
    {
        private readonly IPatientRepository patientRepository;

        public DeletePatientCommandHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public async Task Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await patientRepository.GetByIdAsync(request.Id);
            if (patient == null)
            {
                throw new NotFoundException($"Patient with ID {request.Id} not found.");
            }

            await patientRepository.DeleteAsync(request.Id);
        }
    }
}
