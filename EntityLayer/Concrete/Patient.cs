﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        public int RoleId { get; set; }
        public string PatientName { get; set; }
        public string Mail { get; set; }
        public string UserId { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
