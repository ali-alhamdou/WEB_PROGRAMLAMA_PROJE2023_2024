using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Numerics;

namespace HospitalAutomation.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class AdminController : Controller
    {
        PatientManager _patMan=new PatientManager(new EfPatientRepository());
        DoctorManager _docMan = new DoctorManager(new EfDoctorRepository());
        ReservationManager _resMan=new ReservationManager(new EfReservationRepository());
        DepartmentManager _depMan=new DepartmentManager(new EfDepartmentRepository());
        ShipManager _shipMan=new ShipManager(new EfShipRepository());
        HomeManager _homeMan = new HomeManager(new EfHomeRepository());
        AboutUsManager _aboutMan = new AboutUsManager(new EfAboutUsRepository());
        UserManager _usrMan = new UserManager(new EfUserRepository());
        public readonly IWebHostEnvironment _webHostEnvironment; //Image eklemek için bunu eklememiz lazım
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _webHostEnvironment = webHostEnvironment;
            _userManager=userManager;
        }

        public IActionResult MyProfile()
        {
            var me = _patMan.GetList().FirstOrDefault(x => x.Mail == User.Identity.Name);
            return View(me);
        }
        public IActionResult Patients()
        {

            var values = _patMan.GetList().Where(x=>x.RoleId==3);
            return View(values);
        }
        [HttpPost]
        public async Task<IActionResult> AddDoctor(Doctor doctor, IFormFile? file)
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

            _docMan.TAdd(doctor);
            var patient = _patMan.GetList().FirstOrDefault(x=>x.Mail==doctor.Mail);
            patient.RoleId = 2;
            _patMan.TUpdate(patient);
            return RedirectToAction("Patients", "Admin");
        }
        [HttpGet]
        public IActionResult AddDoctor(int id)
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
            ViewBag.DepartmentID = listDepartments;

            var listShips = new List<SelectListItem>();
            List<Ship> Ships = _shipMan.GetList();
            listShips = Ships.Select(shp => new SelectListItem()
            {
                Value = shp.ShipID.ToString(),
                Text = shp.ShipName
            }).ToList();
            var shpItem = new SelectListItem()
            {
                Value = "",
                Text = "-- SELECT Ship --"
            };
            listShips.Insert(0, shpItem);
            ViewBag.ShipID = listShips;


            var value = _patMan.GetById(id);
            Doctor doctor = new Doctor();
            doctor.DoctorName = value.PatientName;
            doctor.Mail = value.Mail;
            doctor.UserName = value.UserId;

            return View(doctor);
        }
        [HttpGet]
        public IActionResult AboutUs()
        {
            var values = _aboutMan.GetList().FirstOrDefault(k=>k.Id==2);
            return View(values);
        }
        [HttpPost]
        public IActionResult AboutUs(AboutUs aboutUs, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string imagePath = Path.Combine(wwwRootPath, @"img");
            if (file != null)
            {
                using (var fileStream = new FileStream(Path.Combine(imagePath, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                aboutUs.Image = @"\img\" + file.FileName;
            }

            _aboutMan.TUpdate(aboutUs);
            return RedirectToAction("AboutUs", "Admin");
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
        public IActionResult Home()
        {
            var values = _homeMan.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CheckDetail(int id)
        {
            var values = _homeMan.GetList().FirstOrDefault(x => x.Id == id);
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

            var listShips = new List<SelectListItem>();
            List<Ship> Ships = _shipMan.GetList();
            listShips = Ships.Select(shp => new SelectListItem()
            {
                Value = shp.ShipID.ToString(),
                Text = shp.ShipName
            }).ToList();
            var shpItem = new SelectListItem()
            {
                Value = "",
                Text = "-- SELECT Ship --"
            };
            listShips.Insert(0, shpItem);
            ViewBag.Ships = listShips;

            var values = _docMan.ListDoctorWithDepartment().FirstOrDefault(x=>x.DoctorID==id);
            return View(values);
        }
       

        public IActionResult DeleteDetail(int delid)
        {
            var values = _homeMan.GetList().FirstOrDefault(x => x.Id == delid);
            _homeMan.TDelete(values);
            return Json(values);
        }
        [HttpPost]
        public IActionResult CheckDetail(Home home, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string imagePath = Path.Combine(wwwRootPath, @"img");
            if (file != null)
            {
                using (var fileStream = new FileStream(Path.Combine(imagePath, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                home.Image = @"\img\" + file.FileName;
            }
            home.CreationDate=DateTime.Now.Date;
            _homeMan.TUpdate(home);
            return RedirectToAction("CheckDetail", "Admin", home.Id);
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
        public IActionResult Reservations()
        {
            var values = _resMan.ListReservationWithDD().OrderByDescending(x => x.ReservationDay).OrderByDescending(x => x.ReservationTime); ;
            return View(values);
        }
        public IActionResult DeclineReservation(int id)
        {
            var values = _resMan.GetList().FirstOrDefault(x => x.ReservationID == id);
            values.ReservationStatus = false;
            _resMan.TUpdate(values);
            var jsonDepartment = JsonConvert.SerializeObject(values);
            return Json(jsonDepartment);
        }
        [HttpGet]
        public IActionResult NewData()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewData(Home home, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string imagePath = Path.Combine(wwwRootPath, @"img");
            if (file != null)
            {
                using (var fileStream = new FileStream(Path.Combine(imagePath, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                home.Image = @"\img\" + file.FileName;
            }
            home.CreationDate = DateTime.Now.Date;
            _homeMan.TAdd(home);
            return RedirectToAction("Home", "Admin");
        }
    }
}

