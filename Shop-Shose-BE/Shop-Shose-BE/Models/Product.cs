using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string Sex { get; set; }
        public string Description { get; set; }
        public Brand Brand { get; set; }
        public int BrandId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Color_Size_Product> Color_Size_Products { get; set; }
        public ICollection<Cart_of_Product> Cart_of_Products { get; set; }


    }
}
