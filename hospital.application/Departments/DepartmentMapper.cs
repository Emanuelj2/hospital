using hospital.application.DTOs;
using hospital.domain.Organization.entity;

namespace hospital.application.Departments
{
    internal static class DepartmentMapper
    {
        public static DepartmentDto ToDto(Department d) => new()
        {
            Id = d.Id,
            Name = d.Name,
            Location = d.Location,
            HeadEmployeeId = d.HeadEmployeeId,
            HeadEmployeeName = d.Head?.FullName
        };

        public static Department ToEntity(DepartmentDto dto) => new()
        {
            Name = dto.Name,
            Location = dto.Location,
            HeadEmployeeId = dto.HeadEmployeeId
        };

        public static void ApplyTo(Department department, DepartmentDto dto)
        {
            department.Name = dto.Name;
            department.Location = dto.Location;
            department.HeadEmployeeId = dto.HeadEmployeeId;
        }
    }
}
