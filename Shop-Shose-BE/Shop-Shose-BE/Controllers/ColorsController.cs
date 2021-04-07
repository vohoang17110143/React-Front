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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {

        private readonly ShopShoseContext _context;
        public ColorsController(ShopShoseContext shopShoseContext)
        {
            this._context = shopShoseContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllColor()
        {
            var colors = (from c in this._context.Colors
                          select new ColorDTO()
                          {
                              ColorId=c.ColorId,
                              Name=c.Name
                          });
            return Ok(colors);
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetColorByName(string name)
        {
            var colors = await this._context.Colors.Where(c => c.Name.Contains(name)).ToArrayAsync();
            if (colors == null)
            {
                return NotFound(new { message = Message.ErrorFound });
            }
            return Ok(colors);
        }
        [HttpPost]
        public async Task<IActionResult> AddColor([FromBody] Color color)
        {
            if (!await CheckExistColor(color.Name))
            {
                this._context.Colors.Add(color);
                await this._context.SaveChangesAsync();
                return Ok(new { message = Message.Success });
            }
            return BadRequest(new { message = Message.Error });
        }
        public async Task<bool> CheckExistColor(string name)
        {
            var colorExist = await this._context.Colors.Where(c => c.Name == name).FirstOrDefaultAsync();
            if (colorExist!=null)
            {
                return true;
            }
            return false;
        }


    }
}
