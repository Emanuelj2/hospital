using hospital.application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.Interfaces
{
    public interface IPatientService
    {
        Task<List<PatientDto>> GetAllAsync();
        Task<PatientDto> GetByIdAsync(int id);
        Task<PatientDto> CreateAsync(PatientDto patientDto);
        Task<bool> UpdateAsync(int id, PatientDto patientDto);
        Task<bool> DeleteAsync(int id);
    }
}
