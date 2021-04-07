using Shop_Shose_BE.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime DateOrder { get; set; }
        public Status Status { get; set; }
        public ShoppingCar ShoppingCar { get; set; }
        public int ShoppingCarId { get; set; }
    }
}
