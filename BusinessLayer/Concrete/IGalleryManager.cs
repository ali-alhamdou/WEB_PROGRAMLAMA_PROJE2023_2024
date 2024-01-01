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
    public class GalleryManager : IGalleryService
    {
        IGalleryDal _dal;

        public GalleryManager(IGalleryDal dal)
        {
            _dal = dal;
        }

        public Gallery GetById(int id)
        {
            return _dal.GetByID(id);
        }

        public List<Gallery> GetList()
        {
            return _dal.GetList();
        }

        public void TAdd(Gallery t)
        {
            _dal.Insert(t);
        }

        public void TDelete(Gallery t)
        {
            _dal.Delete(t);
        }

        public void TUpdate(Gallery t)
        {
            _dal.Update(t);
        }
    }
}
