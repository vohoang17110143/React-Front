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

namespace Shop_Shose_BE.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ShopShoseContext _context;
        public PostsController(ShopShoseContext shopShoseContext)
        {
            this._context = shopShoseContext;
        }

        [HttpGet("display")]
        public async Task<IActionResult> GetAllPostDisplay()
        {
            try
            {
                var posts = await (from p in this._context.Posts
                                   where p.Display==true
                                   select p).ToArrayAsync();
                if (posts.Count() > 0)
                {

                    return Ok(posts);
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

        [HttpGet]
        public async Task<IActionResult> GetAllPost()
        {
            try
            {
                var posts = await (from p in this._context.Posts
                                   select p).ToArrayAsync();
                if (posts.Count() > 0)
                {

                    return Ok(posts);
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

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPostById(int postId)
        {
            try
            {
                var post = await this._context.Posts.FindAsync(postId);
                if (post != null)
                {

                    return Ok(post);
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
        public async Task<IActionResult> GetPostByTitle(string title)
        {
            try
            {
                var posts = await this._context.Posts.Where(p=>p.Title.Contains(title)).ToArrayAsync();
                if (posts != null)
                {

                    return Ok(posts);
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
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            try
            {
                if (!await CheckExistPost(post.Title))
                {
                    this._context.Posts.Add(post);
                    await this._context.SaveChangesAsync();
                    return Ok(new { message = Message.Success });
                }
                else
                {
                    return BadRequest(new { message = Message.Error });
                }
                
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message});
            }
            
        }
        private async Task<bool> CheckExistPost(string Title)
        {
            var post = await this._context.Posts.FirstOrDefaultAsync(p=>p.Title==Title);
            if (post!=null)
            {
                return true;
            }
            return false;
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            try
            {
                var post = await this._context.Posts.FindAsync(postId);
                if (post != null)
                {
                    post.Display = false;
                    this._context.Posts.Update(post);
                    await this._context.SaveChangesAsync();
                    return NotFound(new { message = Message.Success });
                }
                else
                {
                    return NotFound(new { message = Message.Error});
                }

            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
        
    }
}
