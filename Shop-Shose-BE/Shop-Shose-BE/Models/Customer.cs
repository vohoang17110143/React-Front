using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public ICollection<Account> Accounts { get; set; }
        
        public ICollection<ShoppingCar> ShoppingCars { get; set; }
        public ICollection<Favorite_Product> Favorite_Products { get; set; }
    }
}
