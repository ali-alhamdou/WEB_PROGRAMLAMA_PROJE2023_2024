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
    public class AboutUsManager : IAboutUsService
    {
        IAboutUsDal _dal;

        public AboutUsManager(IAboutUsDal dal)
        {
            _dal = dal;
        }

        public AboutUs GetById(int id)
        {
            return _dal.GetByID(id);
        }

        public List<AboutUs> GetList()
        {
            return _dal.GetList();
        }

        public void TAdd(AboutUs t)
        {
            _dal.Insert(t);
        }

        public void TDelete(AboutUs t)
        {
            _dal.Delete(t);
        }

        public void TUpdate(AboutUs t)
        {
            _dal.Update(t);
        }
    }
}
