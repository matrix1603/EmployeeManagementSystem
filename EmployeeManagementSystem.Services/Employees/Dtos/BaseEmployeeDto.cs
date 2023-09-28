namespace EmployeeManagementSystem.Services.Employees.Dtos
{
    public abstract class BaseEmployeeDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
    }
}
