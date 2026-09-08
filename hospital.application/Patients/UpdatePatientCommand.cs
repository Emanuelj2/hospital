using hospital.application.DTOs;
using hospital.application.Exceptions;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Patients
{
    public record UpdatePatientCommand(int Id, PatientDto Patient) : IRequest;

    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand>
    {
        private readonly IPatientRepository patientRepository;

        public UpdatePatientCommandHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public async Task Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await patientRepository.GetByIdAsync(request.Id);
            if (patient == null)
            {
                throw new NotFoundException($"Patient with ID {request.Id} not found.");
            }

            PatientMapper.ApplyTo(patient, request.Patient);
            await patientRepository.UpdateAsync(patient);
        }
    }
}
