using hospital.application.DTOs;
using hospital.application.Exceptions;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Patients
{
    public record CreatePatientCommand(PatientDto Patient) : IRequest<PatientDto>;

    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, PatientDto>
    {
        private readonly IPatientRepository patientRepository;

        public CreatePatientCommandHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public async Task<PatientDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            // Every admitted patient occupies a room — a patient record can't exist without
            // one being assigned up front. (A patient can still be moved to a null room later,
            // e.g. on discharge — this only gates creation.)
            if (string.IsNullOrWhiteSpace(request.Patient.RoomNumber))
            {
                throw new ValidationException("A room number is required to create a patient.");
            }

            var patient = PatientMapper.ToEntity(request.Patient);
            await patientRepository.AddAsync(patient);

            request.Patient.Id = patient.Id;
            return request.Patient;
        }
    }
}
