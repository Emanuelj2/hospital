using hospital.application.DTOs;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Patients
{
    public record GetAllPatientsQuery : IRequest<List<PatientDto>>;

    public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, List<PatientDto>>
    {
        private readonly IPatientRepository patientRepository;

        public GetAllPatientsQueryHandler(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public async Task<List<PatientDto>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await patientRepository.GetAllAsync();
            return patients.Select(PatientMapper.ToDto).ToList();
        }
    }
}
