using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Gallery
    {
        [Key]
        public int PhotoId { get; set; }
        public string PhotoName { get; set; }
        public string PhotoImage { get; set; }
    }
}
