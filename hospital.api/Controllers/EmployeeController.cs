using hospital.application.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using hospital.application.DTOs;
using hospital.application.Exceptions;
using MediatR;

namespace hospital.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator mediator;

        public EmployeeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> GetAll()
        {
            var employees = await mediator.Send(new GetAllEmployeesQuery());
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            try
            {
                var employee = await mediator.Send(new GetEmployeeByIdQuery(id));
                return Ok(employee);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<EmployeeDto>> GetByEmail(string email)
        {
            var employee = await mediator.Send(new GetEmployeeByEmailQuery(email));
            if (employee == null)
            {
                return NotFound($"Employee with email {email} not found.");
            }
            return Ok(employee);
        }

        [HttpGet("firstname/{firstName}")]
        public async Task<ActionResult<List<EmployeeDto>>> GetByFirstName(string firstName)
        {
            var employees = await mediator.Send(new GetEmployeesByFirstNameQuery(firstName));
            return Ok(employees);
        }

        [HttpGet("lastname/{lastName}")]
        public async Task<ActionResult<List<EmployeeDto>>> GetByLastName(string lastName)
        {
            var employees = await mediator.Send(new GetEmployeesByLastNameQuery(lastName));
            return Ok(employees);
        }

        [HttpGet("phone/{phoneNumber}")]
        public async Task<ActionResult<EmployeeDto>> GetByPhoneNumber(string phoneNumber)
        {
            var employee = await mediator.Send(new GetEmployeeByPhoneNumberQuery(phoneNumber));
            if (employee == null)
            {
                return NotFound($"Employee with phone number {phoneNumber} not found.");
            }
            return Ok(employee);
        }


        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Create(EmployeeDto employeeDto)
        {
            var createdEmployee = await mediator.Send(new CreateEmployeeCommand(employeeDto));
            return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, createdEmployee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDto>> Update(int id, EmployeeDto employeeDto)
        {
            try
            {
                await mediator.Send(new UpdateEmployeeCommand(id, employeeDto));
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
                await mediator.Send(new DeleteEmployeeCommand(id));
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
