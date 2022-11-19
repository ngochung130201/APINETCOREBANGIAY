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
    public class CartsController : ControllerBase
    {
        private readonly BanGiayContext _context;

        public CartsController(BanGiayContext context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return await _context.Carts.OrderByDescending(x =>x.NgayDatHang).ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(Guid id)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            var cartNew =  new Cart
            {
                Id = Guid.NewGuid(),
                CartID = cart.CartID,
                Amount = cart.Amount,
                Name =  cart.Name,
                Image = cart.Image,
                Price = cart.Price,
                PriceSale = cart.PriceSale,
                NgayDatHang = DateTime.Now,

            };
            await _context.Carts.AddAsync(cartNew);
            await _context.SaveChangesAsync();
            return Ok(cartNew);

        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(Guid id)
        {
            return _context.Carts.Any(e => e.Id == id);
        }
    }
}
