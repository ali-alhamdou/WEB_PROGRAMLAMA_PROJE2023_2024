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
    public class UserManager : IUserService
    {
        IUserDal _dal;

        public UserManager(IUserDal dal)
        {
            _dal = dal;
        }

        public ApplicationUser GetById(int id)
        {
            return _dal.GetByID(id);
        }

        public List<ApplicationUser> GetList()
        {
            return _dal.GetList();
        }

        public void TAdd(ApplicationUser t)
        {
            _dal.Insert(t);
        }

        public void TDelete(ApplicationUser t)
        {
            _dal.Delete(t);
        }

        public void TUpdate(ApplicationUser t)
        {
            _dal.Update(t);
        }
    }
}
