using hospital.domain.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.Interfaces
{
    public interface IUserAccountRepository
    {
        Task<UserAccount?> GetByEmailAsync(string email);
        Task<UserAccount?> GetByIdAsync(int id);
        Task AddAsync(UserAccount userAccount);
    }
}
