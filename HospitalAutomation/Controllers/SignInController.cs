using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using HospitalAutomation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAutomation.Controllers
{
    public class SignInController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public SignInController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserSignInViewModel _parameter)
        {
            if(ModelState.IsValid)
            {
                var result = await _signInManager.
                    PasswordSignInAsync(_parameter.username, _parameter.password, true, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            return View();
            
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "SignIn");
        }
    }
}
