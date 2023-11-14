using Microsoft.AspNetCore.Mvc;

namespace HospitalAutomation.Controllers
{
    public class SignInController : Controller
    {
        private readonly 
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index()
        {

        }
    }
}
