using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Ship
    {
        [Key]
        public int ShipID { get; set; }
        public DateTime ShipStart { get; set; }
        public DateTime ShipEnd { get; set; }
        public DateTime BreakStart { get; set; }
        public DateTime BreakEnd { get; set; }
    }
}
