using System.Net;
using EmployeeManagementSystem.Repository.Employees;
using EmployeeManagementSystem.Services.Employees.Dtos;
using EmployeeManagementSystem.Services.Employees.Mapper;
using EmployeeManagementSystem.Services.Employees.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementSystem.Services.Employees
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<ServiceResult<EmployeeDto>> GetByIdAsync(int id)
        {
            var serviceResult = new ServiceResult<EmployeeDto>();

            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);

                if (employee == null)
                {
                    serviceResult.StatusCode = HttpStatusCode.NotFound;
                    return serviceResult;
                }

                serviceResult.Entity = EmployeeMapper.MapToEmployeeDto(employee);
            }
            catch (Exception ex)
            {
                var customError = $"there was an error getting employee with id {id}";
                _logger.LogError(ex, customError);

                serviceResult.AddError(customError);
                serviceResult.StatusCode = HttpStatusCode.InternalServerError;
            }

            return serviceResult;
        }

        public async Task<IReadOnlyCollection<EmployeeDto>> Search(EmployeeSearchDto? query)
        {
            var employeeQueryable = _employeeRepository.GetQueryable();

            if (query != null)
            {
                var validMap = EmployeeMapper.SearchToEntity(query);

                if (validMap != null) { }
                employeeQueryable = employeeQueryable.Where(x =>
                    x.Name.Contains(validMap.Name) && x.Email.Contains(validMap.Email) &&
                    x.Department.Contains(validMap.Department));
            }

            var result = await employeeQueryable.Select(x => EmployeeMapper.MapToEmployeeDto(x)).ToListAsync();

            return result;
        }

        public async Task<ServiceResult<EmployeeDto>> AddAsync(AddEmployeeDto dto)
        {
            var serviceResult = new ServiceResult<EmployeeDto>();
            try
            {
                var validated = new EmployeeValidator().Validate(dto);

                if (!validated.IsValid)
                {
                    serviceResult.AddErrors(validated.Errors.Select(x => x.ErrorMessage).ToList());
                    return serviceResult;
                }

                var res = await _employeeRepository.AddAsync(EmployeeMapper.DtoToEntity(dto));

                serviceResult.Entity = EmployeeMapper.MapToEmployeeDto(res);
            }
            catch (Exception ex)
            {
                serviceResult.AddError(ex.Message);
            }

            return serviceResult;
        }

        public async Task<ServiceResult<EmployeeDto>> UpdateAsync(UpdateEmployeeDto dto)
        {
            var serviceResult = new ServiceResult<EmployeeDto>();
            try
            {
                var validated = new EmployeeValidator().Validate(dto);

                if (!validated.IsValid)
                {
                    serviceResult.AddErrors(validated.Errors.Select(x => x.ErrorMessage).ToList());
                    serviceResult.StatusCode = HttpStatusCode.BadRequest;
                    return serviceResult;
                }

                var employee = await _employeeRepository.GetByIdAsync(dto.Id);

                if (employee == null)
                {
                    serviceResult.StatusCode = HttpStatusCode.NotFound;
                    return serviceResult;
                }

                employee.DateOfBirth = dto.DateOfBirth;
                employee.Name = dto.Name;
                employee.Email = dto.Email;
                employee.Department = dto.Department;

                var result = await _employeeRepository.UpdateAsync(employee);

                serviceResult.Entity = EmployeeMapper.MapToEmployeeDto(result);
            }
            catch (Exception ex)
            {
                var customError = $"there was an error updating employee with id {dto.Id}";
                _logger.LogError(ex, customError);

                serviceResult.AddError(customError);
                serviceResult.StatusCode = HttpStatusCode.InternalServerError;
            }

            return serviceResult;
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var serviceResult = new ServiceResult<bool>();

            try
            {
                var employee = await _employeeRepository.GetByIdAsync(id);

                if (employee == null)
                {
                    serviceResult.StatusCode = HttpStatusCode.NotFound;
                    return serviceResult;
                }

                await _employeeRepository.DeleteAsync(employee);
            }
            catch (Exception ex)
            {
                var customError = $"there was an error deleting employee with id {id}";
                _logger.LogError(ex, customError);

                serviceResult.AddError(customError);
                serviceResult.StatusCode = HttpStatusCode.InternalServerError;
            }

            return serviceResult;
        }
    }
}
