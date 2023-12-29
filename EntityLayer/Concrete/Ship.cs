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
        public string ShipName { get; set; }
        public TimeSpan ShipStart { get; set; }
        public TimeSpan ShipEnd { get; set; }
        public TimeSpan BreakStart { get; set; }
        public TimeSpan BreakEnd { get; set; }
    }
}
