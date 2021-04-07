using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models
{
    public class Cart_of_Product
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdd { get; set; }
        public ShoppingCar ShoppingCar { get; set; }
        public int ShoppingCarId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
