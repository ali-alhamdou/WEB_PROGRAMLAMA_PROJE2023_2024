using EntityLayer.Concrete;
using HospitalAutomation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAutomation.Controllers
{
    public class SignUpController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public SignUpController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel userSUV)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    Email = userSUV.Mail,
                    UserName = userSUV.UserName,
                    NameSurname = userSUV.NameSurname,
                    About = userSUV.About,
                    Image="~/img/pp.jpg",
                    DepartmentID=1,
                    ShipID=1
                };
                var result = await _userManager.CreateAsync(user,userSUV.Password);
                if (result.Succeeded) { 
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(userSUV);
        }
    }
}
