using hospital.application.DTOs;
using hospital.infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace hospital.api.Controllers
{
    // Dev-only endpoint for bootstrapping an empty local database after cloning the repo.
    // Locked to the Development environment — this must never be reachable in a deployed environment,
    // since it has to run without authentication (there's no account to log in with yet).
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly HospitalDbContext context;
        private readonly IWebHostEnvironment environment;

        public SeedController(HospitalDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        [HttpPost]
        public async Task<ActionResult<SeedResultDto>> Seed()
        {
            if (!environment.IsDevelopment())
            {
                return NotFound();
            }

            var result = await DataSeeder.SeedAsync(context);
            return Ok(result);
        }
    }
}
