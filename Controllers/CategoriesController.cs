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
    public class CategoriesController : ControllerBase
    {
        private readonly BanGiayContext _context;

        public CategoriesController(BanGiayContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var result = await _context.Categories.ToListAsync();
            return Ok(result); ;
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var categoryID = await _context.Categories.FindAsync(id);
            if(categoryID==null){
                return NotFound();
            }
            categoryID.Id = category.Id;
            categoryID.Content= category.Content;
            categoryID.Image = category.Image;
            categoryID.Name = category.Name;
            categoryID.Created= DateTime.Now;
            categoryID.Updated= DateTime.Now;
            _context.Categories.Update(categoryID);
            await _context.SaveChangesAsync();
            return NoContent();
           }
         
         
        
    
        

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            var newCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = category.Name,
                Content = category.Content,
                Image = category.Image,
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };
            _context.Add(newCategory);
            await _context.SaveChangesAsync();
            return Ok(newCategory);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
