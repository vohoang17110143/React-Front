using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamToeicOnline_BackEnd_Clients.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Shose_BE.Common;
using Shop_Shose_BE.EF;
using Shop_Shose_BE.Models;
using Shop_Shose_BE.Models.DTO;


namespace Shop_Shose_BE.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopShoseContext _context;
        public ProductsController(ShopShoseContext shopShoseContext)
        {
            _context = shopShoseContext;
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductVM productVM)
        {
            try
            {
                //create product
                Product product = new Product()
                {
                    Name = productVM.Name,
                    Price = productVM.Price,
                    Image = productVM.Image,
                    Sex = productVM.Sex,
                    Description = productVM.Description,
                    BrandId = productVM.BrandId,
                    CategoryId = productVM.CategoryId
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                //create color-size
                List<Color_Size_Product> color_Size_Products = new List<Color_Size_Product>();
                foreach (var color in productVM.Colors)
                {
                    foreach (var size in color.Sizes)
                    {
                        Color_Size_Product color_Size_Product = new Color_Size_Product()
                        {
                            ColorId = color.ColorId,
                            ProductId = product.ProductId,
                            Image = color.Image,
                            SizeId = size.SizeId,
                            Quantity = size.Quantity
                        };
                        color_Size_Products.Add(color_Size_Product);
                    }
                }
                _context.Color_Size_Products.AddRange(color_Size_Products);
                await _context.SaveChangesAsync();
                return Ok(new { message = Message.Success });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        //private async Task<ProductDTO> GetProductById(int id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    var colorSizes = await (from csp in _context.Color_Size_Products
        //                            join c in _context.Colors on csp.ColorId equals c.ColorId
        //                            join s in _context.Sizes on csp.SizeId equals s.SizeId
        //                            where csp.ProductId == product.ProductId
        //                            select new Color_Size_ProductDTO()
        //                            {
        //                                ProductId = csp.ProductId,
        //                                Quantity=csp.Quantity,
        //                                Image=csp.Image,
        //                                ColorName=c.Name,
        //                                SizeNumber=s.SizeNumber

        //                            }).ToListAsync();
        //    ProductDTO productDTO = new ProductDTO(product);    
        //    //var colorSizesDTO = new List<Color_Size_ProductDTO>();
        //    //foreach (var item in colorSizes)
        //    //{
        //    //    var colorDto = new Color_Size_ProductDTO(item);
        //    //    colorSizesDTO.Add(colorDto);
        //    //}

        //    productDTO.Color_Size_ProductDTOs = colorSizes;
        //    return productDTO;
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                List<SizeDTO> sizeDTOList = new List<SizeDTO>();
                List<ProductDTO> productDTOList = new List<ProductDTO>();
                var products = await _context.Products.ToArrayAsync();
                foreach (var product in products)
                {
                    int productId = product.ProductId;
                    var colorDTOs = await (from c in _context.Colors
                                           join csp in _context.Color_Size_Products on c.ColorId equals csp.ColorId
                                           where csp.ProductId == productId
                                           select new ColorDTO()
                                           {
                                               ColorId = c.ColorId,
                                               Name = c.Name,
                                               Image = csp.Image
                                           }).Distinct().ToListAsync();


                    for (int i = 0; i < colorDTOs.Count(); i++)
                    {
                        var sizeDTOs = await (from s in _context.Sizes
                                              join csp in _context.Color_Size_Products on s.SizeId equals csp.SizeId
                                              where csp.ColorId == colorDTOs[i].ColorId && csp.ProductId == productId
                                              select new SizeDTO()
                                              {
                                                  SizeId = s.SizeId,
                                                  SizeNumber = s.SizeNumber,
                                                  Quantity = csp.Quantity

                                              }).ToListAsync();
                        colorDTOs[i].SizeDTOs = sizeDTOs;
                    }
                    ProductDTO productDTO = new ProductDTO(product);
                    productDTO.ColorDTOs = colorDTOs;
                    productDTOList.Add(productDTO);
                }
                return Ok(productDTOList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetAllProductById(int productId)
        {
            try
            {


                var product = await _context.Products.FindAsync(productId);
                var colorDTOs = await (from c in _context.Colors
                                       join csp in _context.Color_Size_Products on c.ColorId equals csp.ColorId
                                       where csp.ProductId == productId
                                       select new ColorDTO()
                                       {
                                           ColorId = c.ColorId,
                                           Name = c.Name,
                                           Image = csp.Image

                                       }).Distinct().ToListAsync();
                List<SizeDTO> sizeDTOList = new List<SizeDTO>();


                for (int i = 0; i < colorDTOs.Count(); i++)
                {
                    var sizeDTOs = await (from s in _context.Sizes
                                          join csp in _context.Color_Size_Products on s.SizeId equals csp.SizeId
                                          where csp.ColorId == colorDTOs[i].ColorId && csp.ProductId == productId
                                          select new SizeDTO()
                                          {
                                              SizeId = s.SizeId,
                                              SizeNumber = s.SizeNumber,
                                              Quantity = csp.Quantity

                                          }).ToListAsync();
                    colorDTOs[i].SizeDTOs = sizeDTOs;
                }
                ProductDTO productDTO = new ProductDTO(product);
                productDTO.ColorDTOs = colorDTOs;



                return Ok(productDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("sex")]
        public async Task<IActionResult> GetProductBySex(string sex)
        {
            try
            {
                List<SizeDTO> sizeDTOList = new List<SizeDTO>();
                List<ProductDTO> productDTOList = new List<ProductDTO>();
                var products = await _context.Products.Where(p => p.Sex == sex).ToArrayAsync();
                foreach (var product in products)
                {
                    int productId = product.ProductId;
                    var colorDTOs = await (from c in _context.Colors
                                           join csp in _context.Color_Size_Products on c.ColorId equals csp.ColorId
                                           where csp.ProductId == productId
                                           select new ColorDTO()
                                           {
                                               ColorId = c.ColorId,
                                               Name = c.Name,
                                               Image = csp.Image
                                           }).Distinct().ToListAsync();


                    for (int i = 0; i < colorDTOs.Count(); i++)
                    {
                        var sizeDTOs = await (from s in _context.Sizes
                                              join csp in _context.Color_Size_Products on s.SizeId equals csp.SizeId
                                              where csp.ColorId == colorDTOs[i].ColorId && csp.ProductId == productId
                                              select new SizeDTO()
                                              {
                                                  SizeId = s.SizeId,
                                                  SizeNumber = s.SizeNumber,
                                                  Quantity = csp.Quantity

                                              }).ToListAsync();
                        colorDTOs[i].SizeDTOs = sizeDTOs;
                    }
                    ProductDTO productDTO = new ProductDTO(product);
                    productDTO.ColorDTOs = colorDTOs;
                    productDTOList.Add(productDTO);
                }
                return Ok(productDTOList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductByCategory(int categoryId)
        {
            try
            {
                List<SizeDTO> sizeDTOList = new List<SizeDTO>();
                List<ProductDTO> productDTOList = new List<ProductDTO>();
                var products = await _context.Products.Where(p => p.CategoryId == categoryId).ToArrayAsync();
                foreach (var product in products)
                {
                    int productId = product.ProductId;
                    var colorDTOs = await (from c in _context.Colors
                                           join csp in _context.Color_Size_Products on c.ColorId equals csp.ColorId
                                           where csp.ProductId == productId
                                           select new ColorDTO()
                                           {
                                               ColorId = c.ColorId,
                                               Name = c.Name,
                                               Image = csp.Image
                                           }).Distinct().ToListAsync();


                    for (int i = 0; i < colorDTOs.Count(); i++)
                    {
                        var sizeDTOs = await (from s in _context.Sizes
                                              join csp in _context.Color_Size_Products on s.SizeId equals csp.SizeId
                                              where csp.ColorId == colorDTOs[i].ColorId && csp.ProductId == productId
                                              select new SizeDTO()
                                              {
                                                  SizeId = s.SizeId,
                                                  SizeNumber = s.SizeNumber,
                                                  Quantity = csp.Quantity

                                              }).ToListAsync();
                        colorDTOs[i].SizeDTOs = sizeDTOs;
                    }
                    ProductDTO productDTO = new ProductDTO(product);
                    productDTO.ColorDTOs = colorDTOs;
                    productDTOList.Add(productDTO);
                }
                return Ok(productDTOList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProduct(string key)
        {
            //try
            //{
            //    string[] keySearch = s.Split('-');
            //    var products = await (from p in _context.Products
            //                          join csp in _context.Color_Size_Products on p.ProductId equals csp.ProductId
            //                          join c in _context.Colors on csp.ColorId equals c.ColorId
            //                          where p.ProductId == Int32.Parse(keySearch[1]) && c.ColorId == Int32.Parse(keySearch[0])
            //                          select new ProductDTO()
            //                          {
            //                              ProductId = p.ProductId,
            //                              Name = p.Name,
            //                              Price = p.Price,
            //                              Image = p.Image,
            //                              Sex = p.Sex,
            //                              //Color_Name = c.Name,
            //                              Description = p.Description
            //                          }).Distinct().FirstOrDefaultAsync();
            //    return Ok(products);
            //}
            //catch (Exception ex)
            //{

            //    return NotFound(new { message = ex.Message });
            //}
            try
            {
                List<SizeDTO> sizeDTOList = new List<SizeDTO>();
                List<ProductDTO> productDTOList = new List<ProductDTO>();
               // var products = await _context.Products.Where(p => p.CategoryId == categoryId).ToArrayAsync();
                string[] keySearch = key.Split('-');
                var products = await (from p in _context.Products
                                      join csp in _context.Color_Size_Products on p.ProductId equals csp.ProductId
                                      join c in _context.Colors on csp.ColorId equals c.ColorId
                                      where p.ProductId == Int32.Parse(keySearch[1]) && c.ColorId == Int32.Parse(keySearch[0])
                                      select p).Distinct().ToArrayAsync();
                foreach (var product in products)
                {
                    int productId = product.ProductId;
                    var colorDTOs = await (from c in _context.Colors
                                           join csp in _context.Color_Size_Products on c.ColorId equals csp.ColorId
                                           where csp.ProductId == productId
                                           select new ColorDTO()
                                           {
                                               ColorId = c.ColorId,
                                               Name = c.Name,
                                               Image = csp.Image
                                           }).Distinct().ToListAsync();


                    for (int i = 0; i < colorDTOs.Count(); i++)
                    {
                        var sizeDTOs = await (from s in _context.Sizes
                                              join csp in _context.Color_Size_Products on s.SizeId equals csp.SizeId
                                              where csp.ColorId == colorDTOs[i].ColorId && csp.ProductId == productId
                                              select new SizeDTO()
                                              {
                                                  SizeId = s.SizeId,
                                                  SizeNumber = s.SizeNumber,
                                                  Quantity = csp.Quantity

                                              }).ToListAsync();
                        colorDTOs[i].SizeDTOs = sizeDTOs;
                    }
                    ProductDTO productDTO = new ProductDTO(product);
                    productDTO.ColorDTOs = colorDTOs;
                    productDTOList.Add(productDTO);
                }
                return Ok(productDTOList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("price")]
        public async Task<IActionResult> GetProductByPrice(double price)
        {
            try
            {
                List<SizeDTO> sizeDTOList = new List<SizeDTO>();
                List<ProductDTO> productDTOList = new List<ProductDTO>();
                var products = await _context.Products.Where(p => p.Price == price).ToArrayAsync();
                foreach (var product in products)
                {
                    int productId = product.ProductId;
                    var colorDTOs = await (from c in _context.Colors
                                           join csp in _context.Color_Size_Products on c.ColorId equals csp.ColorId
                                           where csp.ProductId == productId
                                           select new ColorDTO()
                                           {
                                               ColorId = c.ColorId,
                                               Name = c.Name,
                                               Image = csp.Image
                                           }).Distinct().ToListAsync();


                    for (int i = 0; i < colorDTOs.Count(); i++)
                    {
                        var sizeDTOs = await (from s in _context.Sizes
                                              join csp in _context.Color_Size_Products on s.SizeId equals csp.SizeId
                                              where csp.ColorId == colorDTOs[i].ColorId && csp.ProductId == productId
                                              select new SizeDTO()
                                              {
                                                  SizeId = s.SizeId,
                                                  SizeNumber = s.SizeNumber,
                                                  Quantity = csp.Quantity

                                              }).ToListAsync();
                        colorDTOs[i].SizeDTOs = sizeDTOs;
                    }
                    ProductDTO productDTO = new ProductDTO(product);
                    productDTO.ColorDTOs = colorDTOs;
                    productDTOList.Add(productDTO);
                }
                return Ok(productDTOList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("brand")]
        public async Task<IActionResult> GetProductByBrand(string brandName)
        {
            try
            {
                List<SizeDTO> sizeDTOList = new List<SizeDTO>();
                List<ProductDTO> productDTOList = new List<ProductDTO>();
                var brand = await _context.Brands.Where(b => b.Name == brandName).FirstOrDefaultAsync();
                var products = await _context.Products.Where(p => p.BrandId == brand.BrandId).ToArrayAsync();
                foreach (var product in products)
                {
                    int productId = product.ProductId;
                    var colorDTOs = await (from c in _context.Colors
                                           join csp in _context.Color_Size_Products on c.ColorId equals csp.ColorId
                                           where csp.ProductId == productId
                                           select new ColorDTO()
                                           {
                                               ColorId = c.ColorId,
                                               Name = c.Name,
                                               Image = csp.Image
                                           }).Distinct().ToListAsync();


                    for (int i = 0; i < colorDTOs.Count(); i++)
                    {
                        var sizeDTOs = await (from s in _context.Sizes
                                              join csp in _context.Color_Size_Products on s.SizeId equals csp.SizeId
                                              where csp.ColorId == colorDTOs[i].ColorId && csp.ProductId == productId
                                              select new SizeDTO()
                                              {
                                                  SizeId = s.SizeId,
                                                  SizeNumber = s.SizeNumber,
                                                  Quantity = csp.Quantity

                                              }).ToListAsync();
                        colorDTOs[i].SizeDTOs = sizeDTOs;
                    }
                    ProductDTO productDTO = new ProductDTO(product);
                    productDTO.ColorDTOs = colorDTOs;
                    productDTOList.Add(productDTO);
                }
                return Ok(productDTOList);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
