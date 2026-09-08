using hospital.application.Interfaces;
using hospital.domain.Organization.entity;
using hospital.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace hospital.infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HospitalDbContext hospitalDb;

        public DepartmentRepository(HospitalDbContext hospitalDb)
        {
            this.hospitalDb = hospitalDb;
        }

        public async Task<List<Department>> GetAllAsync() =>
            await hospitalDb.Department.Include(d => d.Head).ToListAsync();

        public async Task<Department?> GetByIdAsync(int id) =>
            await hospitalDb.Department.Include(d => d.Head).FirstOrDefaultAsync(d => d.Id == id);

        public async Task AddAsync(Department department)
        {
            hospitalDb.Department.Add(department);
            await hospitalDb.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            hospitalDb.Department.Update(department);
            await hospitalDb.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await hospitalDb.Department.FindAsync(id);
            if (department != null)
            {
                hospitalDb.Department.Remove(department);
                await hospitalDb.SaveChangesAsync();
            }
        }
    }
}
