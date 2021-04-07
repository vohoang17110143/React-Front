using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.Models.DTO
{
    public class ColorDTO
    {
        public int ColorId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<SizeDTO> SizeDTOs { get; set; }
        public ColorDTO() { }
        public ColorDTO(ColorDTO color)
        {
            ColorId = color.ColorId;
            Name = color.Name;
            Image = color.Image;
        }
    }
}
