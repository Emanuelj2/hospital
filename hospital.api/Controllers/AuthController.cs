using Microsoft.AspNetCore.Mvc;

namespace hospital.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
