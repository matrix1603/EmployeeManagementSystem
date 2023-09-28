using EmployeeManagementSystem.Services.Employees.Dtos;
using FluentValidation;

namespace EmployeeManagementSystem.Services.Employees.Validators
{
    public class EmployeeValidator: AbstractValidator<BaseEmployeeDto>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(5);
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.DateOfBirth).NotNull().NotEmpty();
        }
    }
}
