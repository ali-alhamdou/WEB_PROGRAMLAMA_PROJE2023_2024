using Microsoft.AspNetCore.Mvc;

namespace HospitalAutomation.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Departments()
        {
            return View();
        }
        public IActionResult Doctors()
        {
            return View();
        }
        public IActionResult Ships()
        {
            return View();
        }
        public IActionResult Patients()
        {
            return View();
        }
        public IActionResult Reservations()
        {
            return View();
        }
    }
}
