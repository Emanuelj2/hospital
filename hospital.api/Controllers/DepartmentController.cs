using hospital.application.Departments;
using hospital.application.DTOs;
using hospital.application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hospital.api.Controllers
{
    // Reads (department names/locations) are available to any staff role, since doctors and
    // employees legitimately need them to display on patient/employee records. Writes stay
    // Admin-only.
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator mediator;

        public DepartmentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Employee,Doctor,Admin")]
        public async Task<ActionResult<List<DepartmentDto>>> GetAll()
        {
            var departments = await mediator.Send(new GetAllDepartmentsQuery());
            return Ok(departments);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Employee,Doctor,Admin")]
        public async Task<ActionResult<DepartmentDto>> GetById(int id)
        {
            try
            {
                var department = await mediator.Send(new GetDepartmentByIdQuery(id));
                return Ok(department);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DepartmentDto>> Create(DepartmentDto departmentDto)
        {
            var created = await mediator.Send(new CreateDepartmentCommand(departmentDto));
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, DepartmentDto departmentDto)
        {
            try
            {
                await mediator.Send(new UpdateDepartmentCommand(id, departmentDto));
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await mediator.Send(new DeleteDepartmentCommand(id));
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
