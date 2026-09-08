using hospital.application.DTOs;
using hospital.domain.People;

namespace hospital.application.Patients
{
    internal static class PatientMapper
    {
        public static PatientDto ToDto(Patient p) => new()
        {
            Id = p.Id,
            FirstName = p.FirstName,
            MiddleName = p.MiddelName,
            LastName = p.LastName,
            Gender = p.Gender,
            Email = p.Email,
            DateOfBirth = p.DateOfBirth,
            PhoneNumber = p.PhoneNumber,
            Address = p.Address,
            City = p.City,
            State = p.State,
            ZipCode = p.ZipCode,
            Country = p.Country,
            MedicalRecordNumber = p.MedicalRecordNumber,
            AdmissionDate = p.AdmissionDate,
            DischargeDate = p.DischargeDate,
            Status = p.Status,
            AssignedDoctorId = p.AssignedDoctorId,
            DepartmentId = p.DepartmentId,
            RoomNumber = p.RoomNumber,
            BloodType = p.BloodType,
            Allergies = p.Allergies,
            PrimaryDiagnosis = p.PrimaryDiagnosis,
            InsuranceProvider = p.InsuranceProvider,
            InsurancePolicyNumber = p.InsurancePolicyNumber
        };

        public static Patient ToEntity(PatientDto dto) => new()
        {
            FirstName = dto.FirstName,
            MiddelName = dto.MiddleName,
            LastName = dto.LastName,
            Gender = dto.Gender,
            Email = dto.Email,
            DateOfBirth = dto.DateOfBirth,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            City = dto.City,
            State = dto.State,
            ZipCode = dto.ZipCode,
            Country = dto.Country,
            MedicalRecordNumber = dto.MedicalRecordNumber,
            AdmissionDate = dto.AdmissionDate,
            DischargeDate = dto.DischargeDate,
            Status = dto.Status,
            AssignedDoctorId = dto.AssignedDoctorId,
            DepartmentId = dto.DepartmentId,
            RoomNumber = dto.RoomNumber,
            BloodType = dto.BloodType,
            Allergies = dto.Allergies,
            PrimaryDiagnosis = dto.PrimaryDiagnosis,
            InsuranceProvider = dto.InsuranceProvider,
            InsurancePolicyNumber = dto.InsurancePolicyNumber
        };

        public static void ApplyTo(Patient patient, PatientDto dto)
        {
            patient.FirstName = dto.FirstName;
            patient.MiddelName = dto.MiddleName;
            patient.LastName = dto.LastName;
            patient.Gender = dto.Gender;
            patient.Email = dto.Email;
            patient.DateOfBirth = dto.DateOfBirth;
            patient.PhoneNumber = dto.PhoneNumber;
            patient.Address = dto.Address;
            patient.City = dto.City;
            patient.State = dto.State;
            patient.ZipCode = dto.ZipCode;
            patient.Country = dto.Country;
            patient.MedicalRecordNumber = dto.MedicalRecordNumber;
            patient.AdmissionDate = dto.AdmissionDate;
            patient.DischargeDate = dto.DischargeDate;
            patient.Status = dto.Status;
            patient.AssignedDoctorId = dto.AssignedDoctorId;
            patient.DepartmentId = dto.DepartmentId;
            patient.RoomNumber = dto.RoomNumber;
            patient.BloodType = dto.BloodType;
            patient.Allergies = dto.Allergies;
            patient.PrimaryDiagnosis = dto.PrimaryDiagnosis;
            patient.InsuranceProvider = dto.InsuranceProvider;
            patient.InsurancePolicyNumber = dto.InsurancePolicyNumber;
        }
    }
}
