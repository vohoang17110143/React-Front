using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models.DTO
{
    public class GuestInfoDTO
    {

        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
