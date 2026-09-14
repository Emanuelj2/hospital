using hospital.application.DTOs;
using hospital.application.Exceptions;
using hospital.application.Patients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace hospital.api.Controllers
{
    // Rewritten with MediatR as a demonstration slice — the controller sends a
    // Command/Query and no longer knows how it's fulfilled. Compare against
    // EmployeeController/PatientService for the "before" version of this pattern.
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Employee,Doctor,Admin")]
    public class PatientsController : Controller
    {
        private readonly IMediator mediator;

        #region // Constructor
        public PatientsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        #endregion


        [HttpGet]
        public async Task<ActionResult<List<PatientDto>>> GetAll()
        {
            var patients = await mediator.Send(new GetAllPatientsQuery());
            return Ok(patients);
        }


        // The doctor's own patient list. The doctor id comes from the caller's JWT (via their
        // linked Employee record), never from the request — a doctor can't ask for someone
        // else's patients by passing a different id.
        [HttpGet("assigned-to-me")]
        [Authorize(Roles = "Doctor")]
        public async Task<ActionResult<List<PatientDto>>> GetAssignedToMe()
        {
            var userAccountId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var patients = await mediator.Send(new GetMyAssignedPatientsQuery(userAccountId));
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetById(int id)
        {
            try
            {
                var patient = await mediator.Send(new GetPatientByIdQuery(id));
                return Ok(patient);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<PatientDto>> Create(PatientDto patientDto)
        {
            try
            {
                var createdPatient = await mediator.Send(new CreatePatientCommand(patientDto));
                return CreatedAtAction(nameof(GetById), new { id = createdPatient.Id }, createdPatient);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PatientDto patientDto)
        {
            try
            {
                await mediator.Send(new UpdatePatientCommand(id, patientDto));
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
                await mediator.Send(new DeletePatientCommand(id));
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
