using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class DoctorManager : IDoctorService
    {
        IDoctorDal _dal;

        public DoctorManager(IDoctorDal dal)
        {
            _dal = dal;
        }

        public Doctor GetById(int id)
        {
            return _dal.GetByID(id);
        }
        public List<Doctor> GetByDepartmentID(int id)
        {
            return _dal.GetByDepartmentID(id);
        }

        public List<Doctor> GetList()
        {
            return _dal.GetList();
        }

        public List<Doctor> ListDoctorWithDepartment()
        {
            return _dal.ListDoctorWithDepartment();
        }

        public void TAdd(Doctor t)
        {
            _dal.Insert(t);
        }

        public void TDelete(Doctor t)
        {
            _dal.Delete(t);
        }

        public void TUpdate(Doctor t)
        {
            _dal.Update(t);
        }
    }
}
