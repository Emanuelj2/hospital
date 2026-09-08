using hospital.application.Departments;
using hospital.application.DTOs;
using hospital.application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hospital.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator mediator;

        public DepartmentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentDto>>> GetAll()
        {
            var departments = await mediator.Send(new GetAllDepartmentsQuery());
            return Ok(departments);
        }

        [HttpGet("{id}")]
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
        public async Task<ActionResult<DepartmentDto>> Create(DepartmentDto departmentDto)
        {
            var created = await mediator.Send(new CreateDepartmentCommand(departmentDto));
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
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
