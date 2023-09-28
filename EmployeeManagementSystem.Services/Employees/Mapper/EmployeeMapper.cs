using EmployeeManagementSystem.Models.Employees;
using EmployeeManagementSystem.Services.Employees.Dtos;

namespace EmployeeManagementSystem.Services.Employees.Mapper
{
    public static class EmployeeMapper
    {
        public static EmployeeDto MapToEmployeeDto(Employee entity)
        {
            return new EmployeeDto()
            {
                Id = entity.Id,
                Name = entity.Name,
                DateOfBirth = entity.DateOfBirth,
                Department = entity.Department,
                Email = entity.Email
            };
        }

        public static Employee DtoToEntity(BaseEmployeeDto dto)
        {
            return new Employee()
            {
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                Department = dto.Department,
                Email = dto.Email
            };
        }

        public static EmployeeSearchDto SearchToEntity(EmployeeSearchDto dto)
        {
            
            return new EmployeeSearchDto()
            {
                Name = dto.Name != null ? dto.Name : "",
                Department = dto.Department != null ?  dto.Department : "",
                Email = dto.Email != null ? dto.Email :""
            };
        }
    }
}
