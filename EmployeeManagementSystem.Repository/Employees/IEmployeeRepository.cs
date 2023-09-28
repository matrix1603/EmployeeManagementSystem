using EmployeeManagementSystem.Models.Employees;
using EmployeeManagementSystem.Repository.BaseRepository;

namespace EmployeeManagementSystem.Repository.Employees
{
    public interface IEmployeeRepository : IAsyncRepository<Employee>
    {
    }
}
