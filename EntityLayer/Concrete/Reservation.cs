using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }
        public DateTime ReservationDay { get; set; }
        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }
        public int PatientID { get; set; }
        public Patient Patient { get; set; }
        public int DepartmentID { get; set; }
        public Department Department { get; set; }
    }
}
