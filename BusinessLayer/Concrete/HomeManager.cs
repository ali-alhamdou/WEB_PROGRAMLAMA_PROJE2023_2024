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
    public class HomeManager : IHomeService
    {
        IHomeDal _dal;

        public HomeManager(IHomeDal dal)
        {
            _dal = dal;
        }

        public Home GetById(int id)
        {
            return _dal.GetByID(id);
        }

        public List<Home> GetList()
        {
            return _dal.GetList();
        }

        public void TAdd(Home t)
        {
            _dal.Insert(t);
        }

        public void TDelete(Home t)
        {
            _dal.Delete(t);
        }

        public void TUpdate(Home t)
        {
            _dal.Update(t);
        }
    }
}
