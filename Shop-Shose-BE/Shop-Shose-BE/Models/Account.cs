using Shop_Shose_BE.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DateCreate { get; set; }
        public String Status { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }

    }
}
