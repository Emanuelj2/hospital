using hospital.application.DTOs;
using hospital.application.Interfaces;
using hospital.domain.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public async Task<PatientDto> CreateAsync(PatientDto patientDto)
        {
            var patient = ToEntity(patientDto);
            await patientRepository.AddAsync(patient);
            patientDto.Id = patient.Id;
            return patientDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var patient = await patientRepository.GetByIdAsync(id);
            if(patient == null)
            {
                return false;
            }
            await patientRepository.DeleteAsync(id);
            return true;

        }

        public async Task<List<PatientDto>> GetAllAsync()
        {
            var patients = await patientRepository.GetAllAsync();
            return patients.Select(ToDto).ToList();
        }

        public async Task<PatientDto> GetByIdAsync(int id)
        {
            var patient = await patientRepository.GetByIdAsync(id);
            if(patient == null)
            {
                throw new Exception($"Patient with ID {id} not found.");
            }
            return ToDto(patient);

        }

        public async Task<bool> UpdateAsync(int id, PatientDto patientDto)
        {
            var patient = await patientRepository.GetByIdAsync(id);
            if(patient == null)
            {
                throw new Exception($"Patient with ID {id} not found.");
            }

            patient.FirstName = patientDto.FirstName;
            patient.MiddelName = patientDto.MiddleName;
            patient.LastName = patientDto.LastName;
            patient.Gender = patientDto.Gender;
            patient.Email = patientDto.Email;
            patient.DateOfBirth = patientDto.DateOfBirth;
            patient.PhoneNumber = patientDto.PhoneNumber;
            patient.Address = patientDto.Address;
            patient.City = patientDto.City;
            patient.State = patientDto.State;
            patient.ZipCode = patientDto.ZipCode;
            patient.Country = patientDto.Country;
            patient.MedicalRecordNumber = patientDto.MedicalRecordNumber;
            patient.AdmissionDate = patientDto.AdmissionDate;
            patient.DischargeDate = patientDto.DischargeDate;
            patient.Status = patientDto.Status;
            patient.AssignedDoctorId = patientDto.AssignedDoctorId;
            patient.DepartmentId = patientDto.DepartmentId;
            patient.RoomNumber = patientDto.RoomNumber;
            patient.BloodType = patientDto.BloodType;
            patient.Allergies = patientDto.Allergies;
            patient.PrimaryDiagnosis = patientDto.PrimaryDiagnosis;
            patient.InsuranceProvider = patientDto.InsuranceProvider;
            patient.InsurancePolicyNumber = patientDto.InsurancePolicyNumber;


            await patientRepository.UpdateAsync(patient);
            return true;

            
        }

        private static PatientDto ToDto(Patient p) => new()
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

        private static Patient ToEntity(PatientDto dto) => new()
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
    }
}
