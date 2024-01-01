using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string DoctorAbout { get; set; }
        public string Image { get; set; }
        public string Mail { get; set; }
        public string UserName { get; set; }
        public int? ShipID { get; set; }
        public Ship Ship { get; set; }
        public int? DepartmentID { get; set; }
        public Department Department { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
