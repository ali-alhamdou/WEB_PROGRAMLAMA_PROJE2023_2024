using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAutomation.ViewComponents.Reservation
{
    public class DocForRes : ViewComponent
    {
        DoctorManager _dm = new DoctorManager(new EfDoctorRepository());
        public IViewComponentResult Invoke(int? id)
        {
            var values = _dm.ListDoctorWithDepartment();
            if (id != null)
            {
                values = values.Where(x => x.DepartmentID == id).ToList();
            }
            return View(values);
        }
    }
}
