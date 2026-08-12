using hospital.domain.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task <List<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int employeeId);
        Task<List<Employee>> GetByFirstNameAsync(string firstName);
        Task<List<Employee>> GetByLastNameAsync(string lastName);
        Task<Employee?> GetByEmailAsync(string email);
        Task<Employee?> GetByPhoneNumberAsync(string phoneNumber);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int employeeId);

    }
}
