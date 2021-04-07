using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models
{
    public class Size
    {

        public int SizeId { get; set; }
        public int SizeNumber { get; set; }
        public ICollection<Color_Size_Product> Color_Size_Products { get; set; }
    }
}
