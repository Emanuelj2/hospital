using hospital.application.Interfaces;
using hospital.domain.People;
using hospital.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.infrastructure.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly HospitalDbContext hospitalDbContext;

        #region // Constructor
        public UserAccountRepository(HospitalDbContext hospitalDbContext)
        {
            this.hospitalDbContext = hospitalDbContext;
        }
        #endregion


        public async Task AddAsync(UserAccount userAccount)
        {
            hospitalDbContext.UserAccounts.Add(userAccount);
            await hospitalDbContext.SaveChangesAsync();
        }


        public async Task<UserAccount?> GetByEmailAsync(string email) =>
            await hospitalDbContext.UserAccounts.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);


        public async Task<UserAccount?> GetByIdAsync(int id) =>
            await hospitalDbContext.UserAccounts.FindAsync(id);
    }
}
