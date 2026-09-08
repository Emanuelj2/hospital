using hospital.application.DTOs;
using hospital.domain.People;

namespace hospital.application.Employees
{
    internal static class EmployeeMapper
    {
        public static EmployeeDto ToDto(Employee e) => new()
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

        public static Employee ToEntity(EmployeeDto dto) => new()
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

        public static void ApplyTo(Employee employee, EmployeeDto dto)
        {
            employee.FirstName = dto.FirstName;
            employee.MiddelName = dto.MiddleName;
            employee.LastName = dto.LastName;
            employee.Gender = dto.Gender;
            employee.Email = dto.Email;
            employee.DateOfBirth = dto.DateOfBirth;
            employee.PhoneNumber = dto.PhoneNumber;
            employee.Address = dto.Address;
            employee.City = dto.City;
            employee.State = dto.State;
            employee.ZipCode = dto.ZipCode;
            employee.Country = dto.Country;
            employee.UserAccountId = dto.UserAccountId;
            employee.StateLicenceNumber = dto.StateLicenceNumber;
            employee.Salary = dto.Salary;
            employee.HireDate = dto.HireDate;
            employee.Job = dto.Job;
            employee.EmploymentType = dto.EmploymentType;
            employee.AccessLevel = dto.AccessLevel;
            employee.DepartmentId = dto.DepartmentId;
            employee.Race = dto.Race;
            employee.Ethnicity = dto.Ethnicity;
            employee.VeteranStatus = dto.VeteranStatus;
            employee.Pronoun = dto.Pronoun;
        }
    }
}
