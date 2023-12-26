using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalAutomation.Controllers
{
    public class ReservationController : Controller
    {
        DepartmentManager _dm = new DepartmentManager(new EfDepartmentRepository());
        DoctorManager _docMan = new DoctorManager(new EfDoctorRepository());
        [HttpGet]
        public IActionResult Index(int? id)
        {
            ViewBag.DocIDForRes = id;
            return View();
        }

    }
}
