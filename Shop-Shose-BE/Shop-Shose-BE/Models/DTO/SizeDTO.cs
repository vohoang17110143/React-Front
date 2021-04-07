using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models.DTO
{
    public class SizeDTO
    {
        public int SizeId { get; set; }
        public int SizeNumber { get; set; }
        public int Quantity { get; set; }
        public SizeDTO() { }
        public SizeDTO (SizeDTO size) {
            SizeId = size.SizeId;
            SizeNumber = size.SizeNumber;
            Quantity = size.Quantity;
        } 
    }
}
