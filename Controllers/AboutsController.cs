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
    public class AboutsController : ControllerBase
    {
        private readonly BanGiayContext _context;

        public AboutsController(BanGiayContext context)
        {
            _context = context;
        }

        // GET: api/Abouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<About>>> GetAbouts()
        {  var result =  await _context.Abouts.ToListAsync();
            return Ok(result);
        }

        // GET: api/Abouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<About>> GetAbout(Guid id)
        {
            var about = await _context.Abouts.FindAsync(id);

            if (about == null)
            {
                return NotFound();
            }

            return about;
        }

        // PUT: api/Abouts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbout(Guid id, About about)
        {
            if (id != about.Id)
            {
                return BadRequest();
            }

           var aboutID = await _context.Abouts.FindAsync(id);
           if(aboutID == null) {
            return NotFound();

           }
           aboutID.Id = about.Id;
           aboutID.Content=about.Content;
           aboutID.Image=about.Image;
           aboutID.Facebook=about.Facebook;
           aboutID.Intagram=about.Intagram;
           aboutID.Github=about.Github;
    

            return NoContent();
        }

        // POST: api/Abouts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<About>> PostAbout(About about)
        {
            var newAbout = new About{
                Id=Guid.NewGuid(),
                Content=about.Content,
                Facebook=about.Facebook,
                Github=about.Github,
                Image=about.Image,
                Intagram=about.Intagram,
                Name=about.Name,
            };
            _context.Abouts.Add(newAbout);
            await _context.SaveChangesAsync();
            return Ok(newAbout);
        }

        // DELETE: api/Abouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbout(Guid id)
        {
            var about = await _context.Abouts.FindAsync(id);
            if (about == null)
            {
                return NotFound();
            }

            _context.Abouts.Remove(about);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AboutExists(Guid id)
        {
            return _context.Abouts.Any(e => e.Id == id);
        }
    }
}
