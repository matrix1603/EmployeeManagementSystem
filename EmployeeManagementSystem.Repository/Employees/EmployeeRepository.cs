using EmployeeManagementSystem.Models.Employees;
using EmployeeManagementSystem.Repository.BaseRepository;

namespace EmployeeManagementSystem.Repository.Employees
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeDbContext dbContext) : base(dbContext)
        {
        }
    }
}
