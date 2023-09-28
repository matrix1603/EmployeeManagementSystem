using EmployeeManagementSystem.Models.Employees;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Repository
{
    public class EmployeeDbContext: DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options):base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}