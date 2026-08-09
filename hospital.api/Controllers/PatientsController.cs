using hospital.application.DTOs;
using hospital.application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hospital.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly IPatientService patientService;

        #region // Constructor
        public PatientsController(IPatientService patientService)
        {
            this.patientService = patientService;
        }
        #endregion


        [HttpGet]
        public async Task<ActionResult<List<PatientDto>>> GetAll()
        {
            var patients = await patientService.GetAllAsync();
            return Ok(patients);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDto>> GetById(int id)
        {
            try
            {
                var patient = await patientService.GetByIdAsync(id);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<PatientDto>> Create(PatientDto patientDto)
        {
            var createdPatient = await patientService.CreateAsync(patientDto);
            return CreatedAtAction(nameof(GetById), new { id = createdPatient.Id }, createdPatient);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PatientDto patientDto)
        {
            try
            {
                await patientService.UpdateAsync(id, patientDto);
                return NoContent();
            }
            catch
            { 
                return NotFound(); 
            }

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await patientService.DeleteAsync(id);
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
