using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using HospitalAutomation.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalAutomation.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public readonly IWebHostEnvironment _webHostEnvironment; //Image eklemek için bunu eklememiz lazım
        DepartmentManager _dm = new DepartmentManager(new EfDepartmentRepository());
        DoctorManager _docMan = new DoctorManager(new EfDoctorRepository());

        public AdminPanelController(UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {/*
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel();
            model.NameSurname = values.NameSurname;
            model.About = values.About;
            model.ImageUrl = values.Image;
            model.Mail = values.Email;*/
            return View(/*model*/);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserUpdateViewModel _model, IFormFile? file)
        {//Bunun viewinde @model UserUpdateViewModel yazılıcak
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string animalPath = Path.Combine(wwwRootPath, @"img");
            if (file != null)
            {
                using (var fileStream = new FileStream(Path.Combine(animalPath, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                _model.ImageUrl = @"\img\" + file.FileName;
            }
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
           /* values.ShipID = _model.ShipID;
            values.NameSurname = _model.NameSurname;
            values.About = _model.About;
            values.Image = _model.ImageUrl;
            values.DepartmentID = _model.DepartmentID;
            values.Email = _model.Mail;*/
            var result = await _userManager.UpdateAsync(values);
            if (result.Succeeded)
            {
                TempData["UpdateSuccess"] = "Updated successfully!";
                return RedirectToAction("Index", "AdminPanel");
            }
            else
            {
                TempData["UpdateSuccess"] = "Failed to update!";
                return View();
            }
            return View();
        }
        #region Department
        [HttpGet]
        public IActionResult Departments()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Departments(Department department)
        {
            _dm.TAdd(department);
            return View();
        }
        public IActionResult DepartmentDelete(int id)
        {
            var value = _dm.GetById(id);
            _dm.TDelete(value);
            return RedirectToAction("Departments", "AdminPanel");
        }
        #endregion
        #region Doctor
        [HttpGet]
        public IActionResult Doctors()
        {
            IEnumerable<SelectListItem> DepartmentList = _dm.GetList()
                .Select(k => new SelectListItem
                {
                    Text = k.DepartmentName,
                    Value = k.DepartmentID.ToString()
                });
            ViewBag.Departments = DepartmentList;
            return View();
        }
        [HttpPost]
        public IActionResult Doctors(Doctor doctor, IFormFile? file)
        {
            var error = ViewData.ModelState.Values.SelectMany(modelState => modelState.Errors);
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
            else { doctor.Image = @"\img\pp.jpg"; }
            _docMan.TAdd(doctor);
            return View();
        }
        public IActionResult DoctorDelete(int id)
        {
            var value = _docMan.GetById(id);
            _docMan.TDelete(value);
            return RedirectToAction("Doctors", "AdminPanel");
        }
        [HttpGet]
        public IActionResult DoctorEdit(int id)
        {
            var value = _docMan.GetById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult DoctorEdit(Doctor doctor, IFormFile? file)
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
            return RedirectToAction("Doctors", "AdminPanel");
        }
        #endregion
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
