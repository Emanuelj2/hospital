using hospital.application.DTOs;
using hospital.domain.Enums;
using hospital.domain.Organization.entity;
using hospital.domain.People;
using Microsoft.EntityFrameworkCore;

namespace hospital.infrastructure.Data
{
    // Dev-only helper for bootstrapping a freshly-cloned/empty database.
    // Applies pending migrations, then inserts a small, fixed dataset (idempotent — no-ops if data already exists).
    public static class DataSeeder
    {
        public static async Task<SeedResultDto> SeedAsync(HospitalDbContext context)
        {
            await context.Database.MigrateAsync();

            await ClearExistingDataAsync(context);

            var cardiology = new Department { Name = "Cardiology", Location = "Building A, Floor 3" };
            var emergency = new Department { Name = "Emergency", Location = "Building A, Floor 1" };
            context.Department.AddRange(cardiology, emergency);
            await context.SaveChangesAsync();

            var adminAccount = new UserAccount
            {
                Email = "admin@medihealth.local",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = AccountRole.Admin,
                IsActive = true
            };
            var doctorAccount = new UserAccount
            {
                Email = "doctor@medihealth.local",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor123!"),
                Role = AccountRole.Doctor,
                IsActive = true
            };
            var patientAccount = new UserAccount
            {
                Email = "patient@medihealth.local",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Patient123!"),
                Role = AccountRole.Patient,
                IsActive = true
            };
            context.UserAccounts.AddRange(adminAccount, doctorAccount, patientAccount);
            await context.SaveChangesAsync();

            var admin = new Employee
            {
                FirstName = "Alex",
                LastName = "Rivera",
                Email = adminAccount.Email,
                Gender = "Female",
                DateOfBirth = new DateOnly(1985, 4, 12),
                PhoneNumber = "555-0100",
                Address = "1 Hospital Way",
                City = "Springfield",
                State = "IL",
                ZipCode = "62701",
                Country = "USA",
                StateLicenceNumber = "N/A",
                Salary = 95000m,
                HireDate = DateTime.UtcNow.AddYears(-3),
                Job = JobTitle.Administrator,
                EmploymentType = EmployeeType.FullTime,
                AccessLevel = AccessLevelType.Admin,
                DepartmentId = emergency.Id,
                UserAccountId = adminAccount.Id
            };

            var doctor = new Employee
            {
                FirstName = "Sam",
                LastName = "Okafor",
                Email = doctorAccount.Email,
                Gender = "Male",
                DateOfBirth = new DateOnly(1978, 9, 3),
                PhoneNumber = "555-0101",
                Address = "1 Hospital Way",
                City = "Springfield",
                State = "IL",
                ZipCode = "62701",
                Country = "USA",
                StateLicenceNumber = "IL-DOC-4821",
                Salary = 180000m,
                HireDate = DateTime.UtcNow.AddYears(-6),
                Job = JobTitle.Doctor,
                EmploymentType = EmployeeType.FullTime,
                AccessLevel = AccessLevelType.Elevated,
                DepartmentId = cardiology.Id,
                UserAccountId = doctorAccount.Id
            };

            context.Employees.AddRange(admin, doctor);
            await context.SaveChangesAsync();

            cardiology.HeadEmployeeId = doctor.Id;
            await context.SaveChangesAsync();

            var patient1 = new Patient
            {
                FirstName = "Jamie",
                LastName = "Chen",
                Gender = "Female",
                Email = patientAccount.Email,
                UserAccountId = patientAccount.Id,
                DateOfBirth = new DateOnly(1990, 2, 20),
                PhoneNumber = "555-0200",
                Address = "22 Maple St",
                City = "Springfield",
                State = "IL",
                ZipCode = "62702",
                Country = "USA",
                MedicalRecordNumber = "MRN-0001",
                AdmissionDate = DateTime.UtcNow.AddDays(-2),
                DischargeDate = null,
                Status = PatientStatus.Admitted,
                RoomNumber = "204-A",
                AssignedDoctorId = doctor.Id,
                DepartmentId = cardiology.Id,
                BloodType = BloodType.OPositive,
                Allergies = new List<string> { "Penicillin" },
                InsuranceProvider = "BlueCross",
                InsurancePolicyNumber = "BC-99213"
            };

            var patient2 = new Patient
            {
                FirstName = "Morgan",
                LastName = "Lee",
                Gender = "Male",
                Email = "morgan.lee@example.com",
                DateOfBirth = new DateOnly(1965, 11, 8),
                PhoneNumber = "555-0201",
                Address = "8 Oak Ave",
                City = "Springfield",
                State = "IL",
                ZipCode = "62703",
                Country = "USA",
                MedicalRecordNumber = "MRN-0002",
                AdmissionDate = DateTime.UtcNow.AddDays(-10),
                DischargeDate = DateTime.UtcNow.AddDays(-1),
                Status = PatientStatus.Discharged,
                RoomNumber = "112-B",
                AssignedDoctorId = doctor.Id,
                DepartmentId = emergency.Id,
                BloodType = BloodType.ANegative,
                InsuranceProvider = "Aetna",
                InsurancePolicyNumber = "AE-33871"
            };

            context.Patients.AddRange(patient1, patient2);
            await context.SaveChangesAsync();

            return new SeedResultDto
            {
                Seeded = true,
                Message = "Existing data cleared. Seed data created: 2 departments, 2 employees, 2 patients (3 with login accounts). " +
                           "Login with admin@medihealth.local / Admin123!, doctor@medihealth.local / Doctor123!, or patient@medihealth.local / Patient123!"
            };
        }

        // Wipes every table this seeder owns, in an order that respects FK constraints,
        // so the button can be clicked repeatedly against a database that already has data.
        private static async Task ClearExistingDataAsync(HospitalDbContext context)
        {
            // Department <-> Employee(Head) is a cycle — break it before either side is deleted.
            await context.Department.ExecuteUpdateAsync(d => d.SetProperty(x => x.HeadEmployeeId, x => (int?)null));

            await context.InsuranceClaims.ExecuteDeleteAsync();
            await context.Invoices.ExecuteDeleteAsync();
            await context.Prescriptions.ExecuteDeleteAsync();
            await context.LabResults.ExecuteDeleteAsync();
            await context.Appointments.ExecuteDeleteAsync();

            await context.Patients.ExecuteDeleteAsync();
            await context.Visitors.ExecuteDeleteAsync();

            await context.Employees.ExecuteDeleteAsync();
            await context.Department.ExecuteDeleteAsync();
            await context.UserAccounts.ExecuteDeleteAsync();
        }
    }
}
