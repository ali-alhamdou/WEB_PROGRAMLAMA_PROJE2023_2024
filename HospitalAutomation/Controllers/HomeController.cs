using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using HospitalAutomation.Models;
using HospitalAutomation.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HospitalAutomation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;
        private readonly UserManager<AppUser> _userManager;


        DoctorManager _docMan = new DoctorManager(new EfDoctorRepository());

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, LanguageService localization)
        {
            _logger = logger;
            _userManager = userManager;
            _localization = localization;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult OurDoctors()
        {
            var values = _docMan.ListDoctorWithDepartment();
            return View(values);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #region Localization
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return Redirect(Request.Headers["Referer"].ToString());
        }
        #endregion


    }
}