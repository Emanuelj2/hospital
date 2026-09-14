using hospital.application.DTOs;
using hospital.application.Interfaces;
using MediatR;

namespace hospital.application.Patients
{
    // Resolves the caller's own Employee record from their UserAccountId (taken from the JWT
    // by the controller — never a client-supplied doctor id) and returns only the patients
    // assigned to them. This is what backs the doctor's "My Patients" view.
    public record GetMyAssignedPatientsQuery(int UserAccountId) : IRequest<List<PatientDto>>;

    public class GetMyAssignedPatientsQueryHandler : IRequestHandler<GetMyAssignedPatientsQuery, List<PatientDto>>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IPatientRepository patientRepository;

        public GetMyAssignedPatientsQueryHandler(IEmployeeRepository employeeRepository, IPatientRepository patientRepository)
        {
            this.employeeRepository = employeeRepository;
            this.patientRepository = patientRepository;
        }

        public async Task<List<PatientDto>> Handle(GetMyAssignedPatientsQuery request, CancellationToken cancellationToken)
        {
            var doctor = await employeeRepository.GetByUserAccountIdAsync(request.UserAccountId);
            if (doctor == null)
            {
                return new List<PatientDto>();
            }

            var patients = await patientRepository.GetByAssignedDoctorIdAsync(doctor.Id);
            return patients.Select(PatientMapper.ToDto).ToList();
        }
    }
}
