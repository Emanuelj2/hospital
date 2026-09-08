using hospital.application.DTOs;
using hospital.application.Exceptions;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Patients
{
    public record GetPatientByIdQuery(int Id) : IRequest<PatientDto>;

    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDto>
    {
        private readonly IPatientRepository patientRepository;

        public GetPatientByIdQueryHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public async Task<PatientDto> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await patientRepository.GetByIdAsync(request.Id);
            if (patient == null)
            {
                throw new NotFoundException($"Patient with ID {request.Id} not found.");
            }

            return PatientMapper.ToDto(patient);
        }
    }
}
