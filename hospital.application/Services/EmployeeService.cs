using hospital.application.DTOs;
using hospital.application.Interfaces;
using hospital.domain.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        #region //Constructor
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        #endregion

        public async Task<EmployeeDto> CreateAsync(EmployeeDto employeeDto)
        {
            var employee = ToEntity(employeeDto);
            await employeeRepository.AddAsync(employee);
            employeeDto.Id = employee.Id;
            return employeeDto;
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new Exception($"Employee with ID {id} not found.");
            }
            await employeeRepository.DeleteAsync(id);
        }

        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            var employees = await employeeRepository.GetAllAsync();
            return employees.Select(ToDto).ToList();
        }

        public async Task<EmployeeDto?> GetByEmailAsync(string email)
        {
            var employee = await employeeRepository.GetByEmailAsync(email);
            return employee != null ? ToDto(employee) : null;
        }

        public async Task<List<EmployeeDto>> GetByFirstNameAsync(string firstName)
        {
            var employees = await employeeRepository.GetByFirstNameAsync(firstName);
            return employees.Select(ToDto).ToList();
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new Exception($"Employee with ID {id} not found.");
            }
            return ToDto(employee);
        }

        public async Task<List<EmployeeDto>> GetByLastNameAsync(string lastName)
        {
            var employees = await employeeRepository.GetByLastNameAsync(lastName);
            return employees.Select(ToDto).ToList();
        }

        public async Task<EmployeeDto?> GetByPhoneNumberAsync(string phoneNumber)
        {
            var employee = await employeeRepository.GetByPhoneNumberAsync(phoneNumber);
            return employee != null ? ToDto(employee) : null;
        }

        public async Task UpdateAsync(int id, EmployeeDto employeeDto)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new Exception($"Employee with ID {id} not found.");
            }

            // Update the employee entity with the new values from the DTO
            employee.FirstName = employeeDto.FirstName;
            employee.MiddelName = employeeDto.MiddleName;
            employee.LastName = employeeDto.LastName;
            employee.Gender = employeeDto.Gender;
            employee.Email = employeeDto.Email;
            employee.DateOfBirth = employeeDto.DateOfBirth;
            employee.PhoneNumber = employeeDto.PhoneNumber;
            employee.Address = employeeDto.Address;
            employee.City = employeeDto.City;
            employee.State = employeeDto.State;
            employee.ZipCode = employeeDto.ZipCode;
            employee.Country = employeeDto.Country;
            employee.UserAccountId =employeeDto.UserAccountId;
            employee.StateLicenceNumber = employeeDto.StateLicenceNumber;
            employee.Salary = employeeDto.Salary;
            employee.HireDate = employeeDto.HireDate;
            employee.Job = employeeDto.Job;
            employee.EmploymentType = employeeDto.EmploymentType;
            employee.AccessLevel = employeeDto.AccessLevel;
            employee.DepartmentId =employeeDto.DepartmentId;
            employee.Race =employeeDto.Race;
            employee.Ethnicity =employeeDto.Ethnicity;
            employee.VeteranStatus =employeeDto.VeteranStatus;
            employee.Pronoun =employeeDto.Pronoun;

            await(employeeRepository.UpdateAsync(employee));
        }

        #region //Mapping Methods
        private static EmployeeDto ToDto(Employee e) => new()
        {
            Id = e.Id,
            FirstName = e.FirstName,
            MiddleName = e.MiddelName,
            LastName = e.LastName,
            Gender = e.Gender,
            Email = e.Email,
            DateOfBirth = e.DateOfBirth,
            PhoneNumber = e.PhoneNumber,
            Address = e.Address,
            City = e.City,
            State = e.State,
            ZipCode = e.ZipCode,
            Country = e.Country,
            UserAccountId = e.UserAccountId,
            StateLicenceNumber = e.StateLicenceNumber,
            Salary = e.Salary,
            HireDate = e.HireDate,
            Job = e.Job,
            EmploymentType = e.EmploymentType,
            AccessLevel = e.AccessLevel,
            DepartmentId = e.DepartmentId,
            Race = e.Race,
            Ethnicity = e.Ethnicity,
            VeteranStatus = e.VeteranStatus,
            Pronoun = e.Pronoun
        };

        private static Employee ToEntity(EmployeeDto dto) => new()
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
            UserAccountId = dto.UserAccountId,
            StateLicenceNumber = dto.StateLicenceNumber,
            Salary = dto.Salary,
            HireDate = dto.HireDate,
            Job = dto.Job,
            EmploymentType = dto.EmploymentType,
            AccessLevel = dto.AccessLevel,
            DepartmentId = dto.DepartmentId,
            Race = dto.Race,
            Ethnicity = dto.Ethnicity,
            VeteranStatus = dto.VeteranStatus,
            Pronoun = dto.Pronoun
        };
        #endregion
    }
}
