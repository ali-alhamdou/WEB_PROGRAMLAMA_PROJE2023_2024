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
    public class EfReservationRepository : GenericRepository<Reservation>, IReservationDal
    {
        public List<Reservation> ListReservationWithDD()
        {
            using (var _context = new Context())
            {
                return _context.Reservations.Include(a => a.Department).Include(b => b.Doctor).ToList();
            }
        }
    }
}
