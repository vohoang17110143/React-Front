using Shop_Shose_BE.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models.DTO
{
    public class ProductDTO
    {
        //public int ProductId { get; set; }
        //public string Name { get; set; }
        //public double Price { get; set; }
        //public string Image { get; set; }
        //public string Color_Name { get; set; }
        //public string Sex { get; set; }
        //public int MyProperty { get; set; }
        //public string Description { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public List<ColorDTO> ColorDTOs { get; set; }
        public string Sex { get; set; }
        public string Description { get; set; }
        public ProductDTO() { }
        public ProductDTO(Product product)
        {
            ProductId = product.ProductId;
            Name = product.Name;
            Price = product.Price;
            Image = product.Image;
            Sex = product.Sex;
            Description = product.Description;
        }
    }
}
