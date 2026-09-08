using hospital.application.DTOs;
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
            var patient = PatientMapper.ToEntity(request.Patient);
            await patientRepository.AddAsync(patient);

            request.Patient.Id = patient.Id;
            return request.Patient;
        }
    }
}
