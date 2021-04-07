using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
