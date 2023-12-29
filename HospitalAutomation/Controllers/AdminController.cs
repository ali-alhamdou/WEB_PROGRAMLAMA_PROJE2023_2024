using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Numerics;

namespace HospitalAutomation.Controllers
{
    public class AdminController : Controller
    {
        PatientManager _patMan=new PatientManager(new EfPatientRepository());
        DoctorManager _docMan = new DoctorManager(new EfDoctorRepository());
        ReservationManager _resMan=new ReservationManager(new EfReservationRepository());
        DepartmentManager _depMan=new DepartmentManager(new EfDepartmentRepository());
        ShipManager _shipMan=new ShipManager(new EfShipRepository());
        public readonly IWebHostEnvironment _webHostEnvironment; //Image eklemek için bunu eklememiz lazım

        public AdminController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Departments()
        {
            var values = _depMan.GetList();
            return View(values);
        }
        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            _depMan.TAdd(department);
            var jsonDepartment = JsonConvert.SerializeObject(department);
            return Json(jsonDepartment);
        }
        public IActionResult DeleteDepartment(int delid)
        {
            var values = _depMan.GetList().FirstOrDefault(x=>x.DepartmentID==delid);
            _depMan.TDelete(values);
            return Json(values);
        }
        public IActionResult UpdateDepartment(Department department)
        {
            var values = _depMan.GetList().FirstOrDefault(x => x.DepartmentID == department.DepartmentID);
            values.DepartmentName = department.DepartmentName;
            values.DepartmentAbout = department.DepartmentAbout;
            _depMan.TUpdate(values);
            var jsonDepartment = JsonConvert.SerializeObject(values);
            return Json(jsonDepartment);
        }
        public IActionResult Doctors()
        {
            var values = _docMan.ListDoctorWithDepartment();
            return View(values);
        }
        [HttpGet]
        public IActionResult CheckDoctor(int id)
        {
            var listDepartments = new List<SelectListItem>();
            List<Department> Departments = _depMan.GetList();
            listDepartments = Departments.Select(dept => new SelectListItem()
            {
                Value = dept.DepartmentID.ToString(),
                Text = dept.DepartmentName
            }).ToList();
            var deptItem = new SelectListItem()
            {
                Value = "",
                Text = "-- SELECT DEPARTMENT --"
            };
            listDepartments.Insert(0, deptItem);
            ViewBag.Depts=listDepartments;
            var values = _docMan.ListDoctorWithDepartment().FirstOrDefault(x=>x.DoctorID==id);
            return View(values);
        }
        public IActionResult DeleteDoctor(int delid)
        {
            var values = _docMan.GetList().FirstOrDefault(x => x.DoctorID == delid);
            _docMan.TDelete(values);
            return Json(values);
        }
        [HttpPost]
        public IActionResult CheckDoctor(Doctor doctor, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string imagePath = Path.Combine(wwwRootPath, @"img");
            if (file != null)
            {
                using (var fileStream = new FileStream(Path.Combine(imagePath, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                doctor.Image = @"\img\" + file.FileName;
            }
            _docMan.TUpdate(doctor);
            return RedirectToAction("CheckDoctor", "Admin", doctor.DoctorID);
        }
        public IActionResult Ships()
        {
            var values = _shipMan.GetList();
            return View(values);
        }
        public IActionResult AddShip(Ship ship)
        {
            _shipMan.TAdd(ship);
            var jsonDepartment = JsonConvert.SerializeObject(ship);
            return Json(jsonDepartment);
        }
        public IActionResult DeleteShip(int delid)
        {
            var values = _shipMan.GetList().FirstOrDefault(x => x.ShipID == delid);
            _shipMan.TDelete(values);
            return Json(values);
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
