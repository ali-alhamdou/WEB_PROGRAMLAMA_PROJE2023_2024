using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace HospitalAutomation.Controllers
{
    [Authorize(Roles = UserRoles.Role_Patient)]
    public class ReservationController : Controller
    {
        ReservationManager _resMan = new ReservationManager(new EfReservationRepository());
        DepartmentManager _deptMan = new DepartmentManager(new EfDepartmentRepository());
        DoctorManager _docMan = new DoctorManager(new EfDoctorRepository());
        PatientManager _patMan = new PatientManager(new EfPatientRepository());
        ShipManager _shipMan = new ShipManager(new EfShipRepository());
        
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
        private List<SelectListItem> GetDoctors(int deptId = 0)
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
        public IActionResult Create (Reservation reservation)
        {
            var pati = _patMan.GetList().FirstOrDefault(n=>n.Mail== User.Identity.Name);
            reservation.PatientID = pati.PatientId;
            reservation.ReservationStatus = true;
            _resMan.TAdd(reservation);
            return RedirectToAction(nameof(MyReservations));
        }
        [HttpGet]
        public JsonResult GetDoctorsByDepartment(int deptID)
        {
            List<SelectListItem> doctors = GetDoctors(deptID);
            return Json(doctors);
        }
        public IActionResult Reservation(int docid, DateTime res)
        {
            var doctor = _docMan.ListDoctorWithDepartment().FirstOrDefault(x => x.DoctorID == docid);
            int? shipID = doctor.ShipID;
            var ship = _shipMan.GetList().FirstOrDefault(x => x.ShipID == shipID);
            TimeSpan shipS = ship.ShipStart;
            var min = new TimeSpan(0, 0, 30, 00, 0000000);
            var hour = new TimeSpan(0, 1, 30, 00, 0000000);
            List<TimeSpan> shipTime = new List<TimeSpan>();
            for (int i = 1; i <= 16; i++)
            {
                shipTime.Add(shipS);

                if (i != 8)
                {
                    shipS += min;
                }
                else
                {
                    shipS += hour;
                }
            }
            var reservartions = _resMan.GetList().Where(x => x.DoctorID == docid);
            reservartions = reservartions.Where(y => y.ReservationDay== res);
            reservartions = reservartions.Where(y => y.ReservationStatus== true);
            if (reservartions != null)
            {
                foreach (var item in reservartions)
                {
                    foreach (var item2 in shipTime)
                    {
                        if (item.ReservationTime == item2)
                        {
                            shipTime.Remove(item2);
                            break;
                        }
                    }
                }
            }
            var values = shipTime;
            var jsonDepartments = JsonConvert.SerializeObject(values);
            return Json(jsonDepartments);
        }
        public IActionResult MyReservations(int id = 0)
        {
            var pati = _patMan.GetList().FirstOrDefault(n => n.Mail == User.Identity.Name);
            if (id == 1)
            {
                var active = _resMan.ListReservationWithDD().Where(a => a.PatientID == pati.PatientId).Where(n => n.ReservationStatus == true)
                    .Where(k => (k.ReservationDay.Date == DateTime.Now.Date && k.ReservationTime > DateTime.Now.TimeOfDay) || (k.ReservationDay.Date > DateTime.Now.Date))
                    .OrderByDescending(x => x.ReservationDay).OrderByDescending(x => x.ReservationTime);

                return View(active);
            }
            else if (id == 0)
            {

                var all = _resMan.ListReservationWithDD().Where(a => a.PatientID == pati.PatientId).OrderByDescending(x => x.ReservationDay).OrderByDescending(x => x.ReservationTime);
                return View(all);
            }
            else if (id == 2)
            {

                var today = _resMan.ListReservationWithDD().Where(a => a.PatientID == pati.PatientId).Where(k => k.ReservationDay.Date == DateTime.Now.Date).OrderByDescending(x => x.ReservationDay).OrderByDescending(x => x.ReservationTime);
                return View(today);
            }
            else if (id == 3)
            {

                var decline = _resMan.ListReservationWithDD().Where(a => a.PatientID == pati.PatientId).Where(k => k.ReservationStatus == false).OrderByDescending(x => x.ReservationDay).OrderByDescending(x => x.ReservationTime);
                return View(decline);
            }
            else if (id == 4)
            {

                var today = _resMan.ListReservationWithDD().Where(a => a.Doctor.Mail == User.Identity.Name).Where(k => (k.ReservationDay.Date <= DateTime.Now.Date) || (k.ReservationDay == DateTime.Now.Date && DateTime.Now.TimeOfDay > k.ReservationTime)).OrderByDescending(x => x.ReservationDay).OrderByDescending(x => x.ReservationTime);
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
