using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models
{
    public class Color_Size_Product
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public Color Color { get; set; }
        public int ColorId { get; set; }
        public Size Size { get; set; }
        public int SizeId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
