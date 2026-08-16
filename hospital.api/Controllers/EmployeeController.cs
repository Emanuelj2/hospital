using hospital.application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using hospital.application.DTOs;
using hospital.application.Exceptions;

namespace hospital.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> GetAll()
        {
            var employees = await employeeService.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            try
            {
                var employee = await employeeService.GetByIdAsync(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<EmployeeDto>> GetByEmail(string email)
        {
            var employee = await employeeService.GetByEmailAsync(email);
            if (employee == null)
            {
                return NotFound($"Employee with email {email} not found.");
            }
            return Ok(employee);
        }

        [HttpGet("firstname/{firstName}")]
        public async Task<ActionResult<List<EmployeeDto>>> GetByFirstName(string firstName)
        {
            var employees = await employeeService.GetByFirstNameAsync(firstName);
            return Ok(employees);
        }

        [HttpGet("lastname/{lastName}")]
        public async Task<ActionResult<List<EmployeeDto>>> GetByLastName(string lastName)
        {
            var employees = await employeeService.GetByLastNameAsync(lastName);
            return Ok(employees);
        }

        [HttpGet("phone/{phoneNumber}")]
        public async Task<ActionResult<EmployeeDto>> GetByPhoneNumber(string phoneNumber)
        {
            var employee = await employeeService.GetByPhoneNumberAsync(phoneNumber);
            if (employee == null)
            {
                return NotFound($"Employee with phone number {phoneNumber} not found.");
            }
            return Ok(employee);
        }


        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Create(EmployeeDto employeeDto)
        {
            var createdEmployee = await employeeService.CreateAsync(employeeDto);
            return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, createdEmployee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDto>> Update(int id, EmployeeDto employeeDto)
        {
            try
            {
                await employeeService.UpdateAsync(id, employeeDto);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await employeeService.DeleteAsync(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
