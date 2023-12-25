using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Delete(T t)
        {
            using var _context = new Context();
            _context.Remove(t);
            _context.SaveChanges();
        }

        public T GetByID(int id)
        {
            using var _context = new Context();
            return _context.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            using var _context = new Context();
            return _context.Set<T>().ToList();
        }

        public List<T> GetListByCondition(Expression<Func<T, bool>> filter)
        {
            using var _context = new Context();
            return _context.Set<T>().Where(filter).ToList();
        }

        public void Insert(T t)
        {
            using var _context = new Context();
            _context.Add(t);
            _context.SaveChanges();
        }

        public void Update(T t)
        {
            using var _context = new Context();
            _context.Update(t);
            _context.SaveChanges();
        }
    }
}
