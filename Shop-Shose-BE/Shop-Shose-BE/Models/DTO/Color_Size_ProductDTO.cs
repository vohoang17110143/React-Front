using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models.DTO
{
    public class Color_Size_ProductDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public string ColorName { get; set; }
        public int SizeNumber { get; set; }

        //public Color_Size_ProductDTO(Color_Size_ProductDTO color_Size_ProductDTO)
        //{
        //    ProductId = color_Size_ProductDTO.ProductId;
        //    Quantity = color_Size_ProductDTO.Quantity;
        //    Image = color_Size_ProductDTO.Image;
        //    ColorName = color_Size_ProductDTO.ColorName;
        //    SizeNumber = color_Size_ProductDTO.SizeNumber;
        //}
    }
}
