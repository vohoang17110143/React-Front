using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamToeicOnline_BackEnd_Clients.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Shose_BE.EF;
using Shop_Shose_BE.Models;
using Shop_Shose_BE.Models.DTO;

namespace Shop_Shose_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly ShopShoseContext _context;
        public SizesController(ShopShoseContext shopShoseContext)
        {
            this._context = shopShoseContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSize()
        {
            try
            {
                var sizes = (from s in this._context.Sizes
                             select new SizeDTO()
                             {
                                 SizeId = s.SizeId,
                                 SizeNumber = s.SizeNumber
                             });
                return Ok(sizes);
            }
            catch (Exception ex)
            {

                return NotFound(new { message = ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateSize([FromBody] SizeDTO sizeDTO)
        {
            try
            {
                if (!await CheckExistSize(sizeDTO.SizeNumber))
                {
                    Size size = new Size()
                    {
                        SizeNumber = sizeDTO.SizeNumber
                    };
                    this._context.Sizes.Add(size);
                    await this._context.SaveChangesAsync();
                    return Ok(new { message = Message.Success });
                }
                else
                {
                    return BadRequest(new { message = Message.Exist });
                }

            }
            catch (Exception ex)
            {

                return BadRequest(new { message = Message.Error + "\n" + ex.Message });
            }
        }
        private async Task<bool> CheckExistSize(int sizeNumber)
        {
            var size = await this._context.Sizes.FirstOrDefaultAsync(s => s.SizeNumber == sizeNumber);
            if (size != null)
            {
                return true;
            }
            return false;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSize(int id)
        {
            try
            {
                var size = await this._context.Sizes.FindAsync(id);
                if (size!=null)
                {
                    this._context.Sizes.Remove(size);
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

                return BadRequest(new { message = Message.Error + "\n" + ex.Message });
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditSize(int id, [FromBody] SizeDTO sizeDTO)
        {
            try
            {
                var size = await this._context.Sizes.FindAsync(id);
                this._context.Sizes.Remove(size);
                await this._context.SaveChangesAsync();
                return Ok(new { message = Message.Success });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = Message.Error + "\n" + ex.Message });
            }

        }

    }
}
