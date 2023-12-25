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
    public class DepartmentManager : IDepartmentService
    {
        IDepartmentDal _dal;

        public DepartmentManager(IDepartmentDal dal)
        {
            _dal = dal;
        }

        public Department GetById(int id)
        {
            return _dal.GetByID(id);
        }

        public List<Department> GetList()
        {
            return _dal.GetList();
        }

        public void TAdd(Department t)
        {
            _dal.Insert(t);
        }

        public void TDelete(Department t)
        {
            _dal.Delete(t);
        }

        public void TUpdate(Department t)
        {
            _dal.Update(t);
        }
    }
}
