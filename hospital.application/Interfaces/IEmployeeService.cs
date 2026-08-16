using hospital.application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllAsync();

        // Throws NotFoundException if no employee has this Id
        Task<EmployeeDto> GetByIdAsync(int id);

        Task<List<EmployeeDto>> GetByFirstNameAsync(string firstName);
        Task<List<EmployeeDto>> GetByLastNameAsync(string lastName);

        // These two return null instead of throwing, since they're typically
        // used to check whether an employee already exists (e.g. before create),
        // not to fetch a record that's expected to exist.
        Task<EmployeeDto?> GetByEmailAsync(string email);
        Task<EmployeeDto?> GetByPhoneNumberAsync(string phoneNumber);

        Task<EmployeeDto> CreateAsync(EmployeeDto employeeDto);

        // Throws NotFoundException if no employee has this Id
        Task UpdateAsync(int id, EmployeeDto employeeDto);

        // Throws NotFoundException if no employee has this Id
        Task DeleteAsync(int id);
    }
}
