using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models.DTO
{
    public class ProductDetail
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string For { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Brand_Name { get; set; }
        public string Color_Name { get; set; }
        public ColorDTO Color { get; set; }
        public int Size_Number { get; set; }

    }
}
