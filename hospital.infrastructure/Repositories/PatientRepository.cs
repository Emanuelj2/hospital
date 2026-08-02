using hospital.application.Interfaces;
using hospital.domain.People;
using hospital.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalDbContext hospitalDb;

        #region // Constructor
        public PatientRepository(HospitalDbContext hospitalDb)
        {
            this.hospitalDb = hospitalDb;
        }
        #endregion

        public async Task<List<Patient>> GetAllAsync() =>
            await hospitalDb.Patients.ToListAsync();


        public async Task<Patient?> GetByIdAsync(int patientId) =>
            await hospitalDb.Patients.FindAsync(patientId);

        public async Task AddAsync(Patient patient)
        {
            hospitalDb.Patients.Add(patient);
            await hospitalDb.SaveChangesAsync();
        }

        public async Task DeleteAsync(int patientId)
        {
            var patient = await hospitalDb.Patients.FindAsync(patientId);
            if (patient != null)
            {
                hospitalDb.Patients.Remove(patient);
                await hospitalDb.SaveChangesAsync();

            }
        }

        public async Task UpdateAsync(Patient patient)
        {
            hospitalDb.Patients.Update(patient);
            await hospitalDb.SaveChangesAsync();
        }
    }
}
