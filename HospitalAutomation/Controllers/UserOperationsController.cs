using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using HospitalAutomation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAutomation.Controllers
{
    public class UserOperationsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        UserManager userManager = new UserManager(new EfUserRepository());

        public UserOperationsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Informations()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel _parameter = new UserUpdateViewModel();
          /*  _parameter.ShipID = values.ShipID;
            _parameter.NameSurname = values.NameSurname;
            _parameter.About = values.About;
            _parameter.ImageUrl = values.Image;
            _parameter.DepartmentID = values.DepartmentID;*/
            _parameter.Mail = values.Email;
            return View(_parameter);
        }
        [HttpPost]
        public async Task<IActionResult> Informations(UserUpdateViewModel _parameter)
        {//Bunun viewinde @model UserUpdateViewModel yazılıcak
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
          /*  values.ShipID = _parameter.ShipID;
            values.NameSurname = _parameter.NameSurname;
            values.About = _parameter.About;
            values.Image = _parameter.ImageUrl;
            values.DepartmentID = _parameter.DepartmentID;
            values.Email = _parameter.Mail;
            values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, _parameter.Password);*/
            var result = await _userManager.UpdateAsync(values);
            //result başarılıysa koşulu koy
            return RedirectToAction("Informations","UserOperations");
        }
    }
}
