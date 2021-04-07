using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models
{
    public class Favorite_Product
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public int NumberClick { get; set; }
    }
}
