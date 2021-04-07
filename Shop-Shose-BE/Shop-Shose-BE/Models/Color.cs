using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models
{
    public class Color
    {
        public int ColorId { get; set; }
        public string Name { get; set; }
        public ICollection<Color_Size_Product> Color_Of_Products { get; set; }
    }
}
