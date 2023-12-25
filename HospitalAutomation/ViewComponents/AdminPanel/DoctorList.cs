using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAutomation.ViewComponents.AdminPanel
{
    public class DoctorList : ViewComponent
    {
        DoctorManager _docMan = new DoctorManager(new EfDoctorRepository());
        public IViewComponentResult Invoke()
        {
            var values = _docMan.ListDoctorWithDepartment();
            return View(values);
        }
    }
}
