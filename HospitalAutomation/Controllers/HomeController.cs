using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using HospitalAutomation.Models;
using HospitalAutomation.Services;
using HospitalAutomation.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace HospitalAutomation.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;


        DoctorManager _docMan = new DoctorManager(new EfDoctorRepository());
        HomeManager _homeMan = new HomeManager(new EfHomeRepository());
        AboutUsManager _aboutMan = new AboutUsManager(new EfAboutUsRepository());

        public HomeController(ILogger<HomeController> logger,  LanguageService localization)
        {
            _logger = logger;
            _localization = localization;
        }
        public IActionResult Home()
        {
            var values = _homeMan.GetList().OrderByDescending(x => x.CreationDate);
            return View(values);
        }
        public IActionResult GetDetail(int id)
        {
            Home values = _homeMan.GetList().Where(x => x.Id == id).FirstOrDefault();

            return View(values);
        }
        public IActionResult AboutUs()
        {
            var values = _aboutMan.GetList();
            return View(values);
        }

        public IActionResult OurDoctors()
        {
            var values = _docMan.ListDoctorWithDepartment();
            return View(values);
        }
		public IActionResult GetDoctor(int id)
		{
			Doctor values = _docMan.ListDoctorWithDepartment().Where(x=>x.DoctorID==id).FirstOrDefault();

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