using hospital.application.Interfaces;
using hospital.domain.People;
using hospital.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace hospital.infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HospitalDbContext hospitalDb;

        public EmployeeRepository(HospitalDbContext hospitalDb)
        {
            this.hospitalDb = hospitalDb;
        }

        public async Task<List<Employee>> GetAllAsync() =>
            await hospitalDb.Employees.ToListAsync();

        public async Task<Employee?> GetByIdAsync(int employeeId) =>
            await hospitalDb.Employees.FindAsync(employeeId);

        public async Task<List<Employee>> GetByFirstNameAsync(string firstName) =>
            await hospitalDb.Employees
                .Where(e => e.FirstName == firstName)
                .ToListAsync();

        public async Task<List<Employee>> GetByLastNameAsync(string lastName) =>
            await hospitalDb.Employees
                .Where(e => e.LastName == lastName)
                .ToListAsync();

        public async Task<Employee?> GetByEmailAsync(string email) =>
            await hospitalDb.Employees.FirstOrDefaultAsync(e => e.Email == email);

        public async Task<Employee?> GetByPhoneNumberAsync(string phoneNumber) =>
            await hospitalDb.Employees.FirstOrDefaultAsync(e => e.PhoneNumber == phoneNumber);

        public async Task AddAsync(Employee employee)
        {
            hospitalDb.Employees.Add(employee);
            await hospitalDb.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            hospitalDb.Employees.Update(employee);
            await hospitalDb.SaveChangesAsync();
        }

        public async Task DeleteAsync(int employeeId)
        {
            var employee = await hospitalDb.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                hospitalDb.Employees.Remove(employee);
                await hospitalDb.SaveChangesAsync();
            }
        }
    }
}