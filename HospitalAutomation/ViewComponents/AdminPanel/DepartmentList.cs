using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAutomation.ViewComponents.AdminPanel
{
    public class Departments : ViewComponent
    {
        DepartmentManager _dm = new DepartmentManager(new EfDepartmentRepository());
        public IViewComponentResult Invoke()
        {
            var values = _dm.GetList();
            return View(values);
        }
    }
}
