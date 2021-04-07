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
    public class CategoriesController : ControllerBase
    {
        private readonly ShopShoseContext _context;
        public CategoriesController(ShopShoseContext shopShoseContext)
        {
            this._context = shopShoseContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var categories = await (from cate in this._context.Categories
                                        select new CategoryDTO() { 
                                            CategoryId=cate.CategoryId,
                                            Name=cate.Name
                                        }).ToListAsync();
                if (categories.Count()>0)
                {
                    return Ok(categories);
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
        [HttpGet("{cateId}")]
        public async Task<IActionResult> GetCategoryById(int cateId)
        {
            try
            {
                var category = await (from cate in this._context.Categories
                                        where cate.CategoryId==cateId
                                        select new CategoryDTO()
                                        {
                                            CategoryId = cate.CategoryId,
                                            Name = cate.Name
                                        }).FirstOrDefaultAsync();//defauld return []

                if (category != null)
                {
                    return Ok(category);
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
        [HttpGet("search")]
        public async Task<IActionResult> GetCategoryByName(string nameCate)
        {
            try
            {
                var categories = await (from cate in this._context.Categories
                                  where cate.Name.Contains(nameCate)
                                  select new CategoryDTO() {
                                      CategoryId = cate.CategoryId,
                                      Name = cate.Name
                                  }).ToArrayAsync();//defauld return []

                if (categories.Count() > 0)
                {
                    return Ok(categories);
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

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (!await CheckCateExist(categoryDTO.Name))
            {
                Category category = new Category()
                {
                    Name = categoryDTO.Name
                };
                this._context.Categories.Add(category);
                await this._context.SaveChangesAsync();
                return Ok(new { message = Message.Success });
            }
            else
            {
                return BadRequest(new { message = Message.Error});
            }
        }

        public async Task<bool> CheckCateExist(string nameCate)
        {
            var cate = await this._context.Categories.FirstOrDefaultAsync(cate => cate.Name == nameCate);
            if (cate!=null)
            {
                return true;
            }
            return false;
        }

        [HttpDelete("{cateId}")]
        public async Task<IActionResult> CreateCategory(int cateId)
        {
            
            try
            {
                var cate = await this._context.Categories.FindAsync(cateId);
                if (cate != null)
                {
                    this._context.Categories.Remove(cate);
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
        [HttpPut("{cateId}")]
        public async Task<IActionResult> EditCategory(int cateId, [FromBody]CategoryDTO categoryDTO)
        {
            try
            {
                var cate = await this._context.Categories.FindAsync(cateId);
                if (cate != null)
                {
                    cate.Name = categoryDTO.Name;
                    this._context.Categories.Update(cate);
                    await this._context.SaveChangesAsync();
                    return Ok(new { message = Message.Success });
                }
                else
                {
                    return NotFound(new { message = Message.ErrorFound});
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
        

    }
}
