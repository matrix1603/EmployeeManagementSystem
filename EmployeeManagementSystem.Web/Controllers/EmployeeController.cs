using EmployeeManagementSystem.Services.Employees;
using EmployeeManagementSystem.Services.Employees.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagementSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ActionResult<IEnumerable<BaseEmployeeDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ActionResult))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ActionResult))]
        public async Task<ActionResult<IEnumerable<BaseEmployeeDto>>> GetAsync([FromQuery] EmployeeSearchDto? query)
        {
            return Ok(await _employeeService.Search(query));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ActionResult<BaseEmployeeDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ActionResult))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ActionResult))]
        public async Task<ActionResult<BaseEmployeeDto>> GetAsync(int id)
        {
            var result = await _employeeService.GetByIdAsync(id);
            
            return StatusCode((int)result.StatusCode, result.StatusCode == HttpStatusCode.OK ? result.Entity: result.ErrorMessage);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ActionResult<BaseEmployeeDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ActionResult))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ActionResult))]
        public async Task<ActionResult<BaseEmployeeDto>> PostAsync([FromBody] AddEmployeeDto employeeDto)
        {
            var result = await _employeeService.AddAsync(employeeDto);

            return StatusCode((int)result.StatusCode, result.StatusCode == HttpStatusCode.OK ? result.Entity : result.ErrorMessage);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ActionResult<BaseEmployeeDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ActionResult))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ActionResult))]
        public async Task<ActionResult<BaseEmployeeDto>> PutAsync([FromBody] UpdateEmployeeDto employeeDto)
        {
            var result = await _employeeService.UpdateAsync(employeeDto);

            return StatusCode((int)result.StatusCode, result.StatusCode == HttpStatusCode.OK ? result.Entity : result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result =  await _employeeService.DeleteAsync(id);

            return StatusCode((int)result.StatusCode, result.StatusCode == HttpStatusCode.OK ? null : result.ErrorMessage);
        }
    }
}
