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
    public class ShipManager : IShipService
    {
        IShipDal _dal;

        public ShipManager(IShipDal dal)
        {
            _dal = dal;
        }

        public Ship GetById(int id)
        {
            return _dal.GetByID(id);
        }

        public List<Ship> GetList()
        {
            return _dal.GetList();
        }

        public void TAdd(Ship t)
        {
            _dal.Insert(t);
        }

        public void TDelete(Ship t)
        {
            _dal.Delete(t);
        }

        public void TUpdate(Ship t)
        {
            _dal.Update(t);
        }
    }
}
