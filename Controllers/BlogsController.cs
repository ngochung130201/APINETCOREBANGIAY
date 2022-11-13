using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BanGiay.Data;
using BanGiay.Models;

namespace BanGiay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BanGiayContext _context;

        public BlogsController(BanGiayContext context)
        {
            _context = context;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            var result = await _context.Blogs.ToListAsync();
            return Ok(result);
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(Guid id)
        {
          var blog = await _context.Blogs.FindAsync(id);
         

            if (blog == null)
            {
                return NotFound();
            }
         

            return Ok(blog);
        }

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(Guid id, Blog blog)
        {
            if (id != blog.Id)
            {
                return BadRequest();
            }
         var blogID = await _context.Blogs.FindAsync(id);
         if(blogID == null){
            return NotFound();
         }
         blogID.Id=blog.Id;
         blogID.Content=blog.Content;
         blogID.Description=blog.Description;
         blogID.Gallery=blog.Gallery;
         blogID.Name=blog.Name;
         blogID.SLug=blog.SLug;
         blogID.Image=blog.Image;
         blogID.Created=DateTime.Now;
         blogID.Updated=DateTime.Now;
         _context.Blogs.Update(blogID);
         await _context.SaveChangesAsync();
         return NoContent();
        }

        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog(Blog blog)
        {
           var newBlog = new Blog{
            Id = Guid.NewGuid(),
            Content = blog.Content,
            Description=blog.Description,
            Gallery= blog.Gallery,
            Image= blog.Image,
            Name= blog.Name,
            SLug= blog.SLug,
            Created= DateTime.Now,
            Updated= DateTime.Now
           };
           _context.Blogs.Add(newBlog);
           await _context.SaveChangesAsync();
           return Ok(newBlog);
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogExists(Guid id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
