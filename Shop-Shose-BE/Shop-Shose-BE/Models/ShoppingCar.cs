using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models
{
    public class ShoppingCar
    {
        public int ShoppingCarId { get; set; }
        public double TotalCost { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart_of_Product> Cart_of_Products { get; set; }
    }
}
