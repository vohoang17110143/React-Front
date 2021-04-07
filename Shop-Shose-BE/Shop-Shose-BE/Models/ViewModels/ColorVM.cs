using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models.ViewModels
{
    public class ColorVM
    {
        public int ColorId { get; set; }
        public string Image { get; set; }
        public List<SizeVM> Sizes { get; set; }
    }
}
