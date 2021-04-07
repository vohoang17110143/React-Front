using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamToeicOnline_BackEnd_Clients.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Shose_BE.EF;
using Shop_Shose_BE.Models;
using Shop_Shose_BE.Models.DTO;


namespace Shop_Shose_BE.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly ShopShoseContext _context;
        public BrandsController(ShopShoseContext shopShoseContext)
        {
            this._context = shopShoseContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrand()
        {
            try
            {
                var brands = await (from b in this._context.Brands
                              select new BrandDTO()
                              {
                                  BrandId = b.BrandId,
                                  Name = b.Name
                              }).ToArrayAsync();
                if (brands.Count()>0)
                {
                    return Ok(brands);
                }
                else
                {
                    return NotFound(new { message = Message.ErrorFound });
                }
               

            }
            catch (Exception ex)
            {

                return NotFound(new { message = ex.Message });
            }
            
        }

        [HttpGet("{brandId}")]
        public async Task<IActionResult> GetBrandById(int brandId)
        {
            try
            {
                var brands = await (from b in this._context.Brands
                                    where b.BrandId==brandId
                                    select new BrandDTO()
                                    {
                                        BrandId = b.BrandId,
                                        Name = b.Name
                                    }).FirstOrDefaultAsync();
                if (brands!=null)
                {
                    return Ok(brands);
                }
                else
                {
                    return NotFound(new { message = Message.ErrorFound });
                }


            }
            catch (Exception ex)
            {

                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetBrandByName(string name)
        {
            try
            {
                var brands = await (from b in this._context.Brands
                              where b.Name.Contains(name)
                              select new BrandDTO()
                              {
                                  BrandId = b.BrandId,
                                  Name = b.Name
                              }).ToArrayAsync();
                if (brands.Count() > 0)
                {
                    return Ok(brands);
                }
                else
                {
                    return NotFound(new { message = Message.ErrorFound });
                }


            }
            catch (Exception ex)
            {

                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] BrandDTO brandDTO)
        {
            if (!await CheckBrandExist(brandDTO.Name))
            {
                Brand brand = new Brand()
                {
                    Name = brandDTO.Name
                };
                this._context.Brands.Add(brand);
                await this._context.SaveChangesAsync();
                return Ok(new { message = Message.Success });
            }
            else
            {
                return BadRequest(new { message = Message.Error });
            }
            
        }

        public async Task<bool> CheckBrandExist(string nameBrand)
        {
            var brand = await this._context.Brands.FirstOrDefaultAsync(b => b.Name == nameBrand);
            if (brand!=null)
            {
                return true;
            }
            return false;
        }


        [HttpPut("{brandId}")]
        public async Task<IActionResult> EditBrand(int brandId, [FromBody] BrandDTO brandDTO)
        {
            try
            {
                var brand = await this._context.Brands.FindAsync(brandId);
                if (brand != null)
                {
                    brand.Name = brandDTO.Name;
                    this._context.Brands.Update(brand);
                    await this._context.SaveChangesAsync();
                    return Ok(new { message = Message.Success });
                }
                else
                {
                    return NotFound(new { message = Message.ErrorFound });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                var brand = await this._context.Brands.FindAsync(id);
                if (brand != null)
                {
                    this._context.Brands.Remove(brand);
                    await this._context.SaveChangesAsync();
                    return Ok(new { message = Message.Success });
                }
                else
                {
                    return NotFound(new { message = Message.ErrorFound });
                }
            }
            catch (Exception ex)
            {

                return NotFound(new { message = ex.Message });
            }
        }
    }
}
