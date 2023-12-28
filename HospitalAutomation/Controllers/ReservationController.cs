using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalAutomation.Controllers
{
    public class ReservationController : Controller
    {
        ReservationManager _resMan = new ReservationManager(new EfReservationRepository());
        DepartmentManager _deptMan = new DepartmentManager(new EfDepartmentRepository());
        DoctorManager _docMan = new DoctorManager(new EfDoctorRepository());
        PatientManager _patMan = new PatientManager(new EfPatientRepository());
        public IActionResult Index()
        {
            List<Reservation> reservations = _resMan.ListReservationWithDD();
            return View(reservations);
        }
        private List<SelectListItem> GetDepartments()
        {
            var listDepartments = new List<SelectListItem>();
            List<Department> Departments = _deptMan.GetList();
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
            return listDepartments;
        }
        private List<SelectListItem> GetDoctors(int deptId = 1)
        {
            var listDoctors = new List<SelectListItem>();
            List<Doctor> Doctors = _docMan.ListDoctorWithDepartment();
            listDoctors = Doctors.Where(x => x.DepartmentID == deptId).OrderBy(y => y.DoctorName)
                .Select(n =>
                    new SelectListItem
                    {
                        Value = n.DoctorID.ToString(),
                        Text = n.DoctorName
                    }
                ).ToList();
            var docItem = new SelectListItem()
            {
                Value = "",
                Text = "-- SELECT DOCTOR --"
            };
            listDoctors.Insert(0, docItem);
            return listDoctors;
        }
        [HttpGet]
        public IActionResult Create()
        {
            Reservation reservation = new Reservation();
            ViewBag.DepartmentID = GetDepartments();
            ViewBag.DoctorID = GetDoctors();
            return View(reservation);
        }
        [HttpPost]
        public IActionResult Create(Reservation reservation)
        {
            reservation.PatientID = 4;
            _resMan.TAdd(reservation);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public JsonResult GetDoctorsByDepartment(int deptID)
        {
            List<SelectListItem> doctors = GetDoctors(deptID);
            return Json(doctors);
        }
    }

}
