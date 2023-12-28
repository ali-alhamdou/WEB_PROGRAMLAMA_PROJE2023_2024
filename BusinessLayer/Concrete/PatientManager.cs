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
    public class PatientManager : IPatientService
    {
        IPatientDal _dal;

        public PatientManager(IPatientDal dal)
        {
            _dal = dal;
        }

        public Patient GetById(int id)
        {
            return _dal.GetByID(id);
        }

        public List<Patient> GetList()
        {
            return _dal.GetList();
        }

        public void TAdd(Patient t)
        {
            _dal.Insert(t);
        }

        public void TDelete(Patient t)
        {
            _dal.Delete(t);
        }

        public void TUpdate(Patient t)
        {
            _dal.Update(t);
        }
    }
}
