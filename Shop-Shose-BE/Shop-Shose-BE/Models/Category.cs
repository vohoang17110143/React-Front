using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Favorite_Product> Favorite_Products { get; set; }
    }
}
