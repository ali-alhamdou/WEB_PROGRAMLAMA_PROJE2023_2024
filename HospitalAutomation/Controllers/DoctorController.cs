using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HospitalAutomation.Controllers
{
    [Authorize(Roles = UserRoles.Role_Doctor)]
    public class DoctorController : Controller
    {
        DoctorManager _docMan = new DoctorManager(new EfDoctorRepository());
        DepartmentManager _depMan = new DepartmentManager(new EfDepartmentRepository());
        ReservationManager _resMan = new ReservationManager(new EfReservationRepository());
        ShipManager _shipMan = new ShipManager(new EfShipRepository());
        public readonly IWebHostEnvironment _webHostEnvironment; //Image eklemek için bunu eklememiz lazım

        public DoctorController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult MyProfile()
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
            ViewBag.Depts = listDepartments;

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
            var doC = _docMan.GetList().FirstOrDefault(x=>x.Mail==User.Identity.Name);
            int docID = doC.DoctorID;
            var values = _docMan.ListDoctorWithDepartment().FirstOrDefault(x => x.DoctorID == docID);
            return View(values);
        }
        [HttpPost]
        public IActionResult MyProfile(Doctor doctor, IFormFile? file)
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
            return RedirectToAction("MyProfile", "Doctor", doctor.DoctorID);
        }
        public IActionResult Reservations(int id=0)
        {
            if (id == 1)
            {
                var active  = _resMan.ListReservationWithDD().Where(a => a.Doctor.Mail == User.Identity.Name).Where(n=>n.ReservationStatus==true)
                    .Where(k=>(k.ReservationDay.Date == DateTime.Now.Date && k.ReservationTime > DateTime.Now.TimeOfDay)||(k.ReservationDay.Date > DateTime.Now.Date))
                    .OrderByDescending(x => x.ReservationDay).OrderByDescending(x => x.ReservationTime);
                
                return View(active);
            }else if(id == 0)
            {

                var all = _resMan.ListReservationWithDD().Where(a => a.Doctor.Mail == User.Identity.Name).OrderByDescending(x => x.ReservationDay).OrderByDescending(x => x.ReservationTime);
                return View(all);
            }
            else if (id == 2)
            {

                var today = _resMan.ListReservationWithDD().Where(a => a.Doctor.Mail == User.Identity.Name).Where(k => k.ReservationDay.Date == DateTime.Now.Date).OrderByDescending(x => x.ReservationDay).OrderByDescending(x => x.ReservationTime);
                return View(today);
            }
            else if (id == 3)
            {

                var decline = _resMan.ListReservationWithDD().Where(a => a.Doctor.Mail == User.Identity.Name).Where(k => k.ReservationStatus == false).OrderByDescending(x => x.ReservationDay).OrderByDescending(x => x.ReservationTime);
                return View(decline);
            }
            else if (id == 4)
            {

                var today = _resMan.ListReservationWithDD().Where(a => a.Doctor.Mail == User.Identity.Name).Where(k => (k.ReservationDay.Date <= DateTime.Now.Date)||(k.ReservationDay==DateTime.Now.Date && DateTime.Now.TimeOfDay>k.ReservationTime)).OrderByDescending(x => x.ReservationDay).OrderByDescending(x => x.ReservationTime);
                return View(today);
            }
            return View();
        }
        public IActionResult DeclineReservation(int id)
        {
            var values = _resMan.GetList().FirstOrDefault(x => x.ReservationID == id);
            values.ReservationStatus = false;
            _resMan.TUpdate(values);
            var jsonDepartment = JsonConvert.SerializeObject(values);
            return Json(jsonDepartment);
        }

    }
}
