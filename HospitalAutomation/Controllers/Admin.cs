using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HospitalAutomation.Controllers
{
    public class Admin : Controller
    {
        PatientManager _patMan=new PatientManager(new EfPatientRepository());
        DoctorManager _docMan = new DoctorManager(new EfDoctorRepository());
        ReservationManager _resMan=new ReservationManager(new EfReservationRepository());
        DepartmentManager _depMan=new DepartmentManager(new EfDepartmentRepository());
        public IActionResult Index()
        {
            return View();
        }/*
        public IActionResult Departments()
        {
            var values = _depMan.GetList();
            return View(values);
        }*/
        public IActionResult Departments()
        {
            return View();
        }
        public IActionResult GetDepartments()
        {
            var values = _depMan.GetList();
            var jsonDepartments=JsonConvert.SerializeObject(values);
            return Json(jsonDepartments);
        }
        public IActionResult GetDepartmentById(int depid)
        {
            var values = _depMan.GetList().FirstOrDefault(x=>x.DepartmentID==depid);
            var jsonDepartment= JsonConvert.SerializeObject(values);
            return Json(jsonDepartment);
        }
        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            _depMan.TAdd(department);
            var jsonDepartment = JsonConvert.SerializeObject(department);
            return Json(jsonDepartment);
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
