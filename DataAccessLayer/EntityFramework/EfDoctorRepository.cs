using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfDoctorRepository : GenericRepository<Doctor>, IDoctorDal
    {
        public List<Doctor> GetByDepartmentID(int id)
        {
            using (var _context = new Context())
            {
                return _context.Doctors.Where(x=>x.DepartmentID==id).ToList();
            }
        }
        public List<Doctor> ListDoctorWithDepartment()
        {
            using (var _context = new Context())
            {
                return _context.Doctors.Include(b => b.Department).ToList();
            }
        }
    }
}
