using EmployeeManagementSystem.Services.Employees.Dtos;

namespace EmployeeManagementSystem.Services.Employees
{
    public interface IEmployeeService
    {
        Task<ServiceResult<EmployeeDto>> GetByIdAsync(int id);
        Task<IReadOnlyCollection<EmployeeDto>> Search(EmployeeSearchDto? query);
        Task<ServiceResult<EmployeeDto>> AddAsync(AddEmployeeDto dto);
        Task<ServiceResult<EmployeeDto>> UpdateAsync(UpdateEmployeeDto dto);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}
