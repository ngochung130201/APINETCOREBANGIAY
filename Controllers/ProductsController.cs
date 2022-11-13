using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BanGiay.Data;
using BanGiay.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BanGiay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly BanGiayContext _context;
        public static int PAGE_SIZE { get; set; } = 5;
        public ProductsController(BanGiayContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetALLProducts(string ?search, double? from, double? to, string ?sort,int page)
        {
            var result = _context.Products.AsQueryable();

            if (!String.IsNullOrEmpty(search))
            {
                result = result.Where(x => x.Name.Contains(search)

               );
            }
            if (from.HasValue)
            {
                result = result.Where(x => x.Price >= from);
            }
            if (to.HasValue)
            {
                result = result.Where(x => x.Price <= from);
            }
            if (!String.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "price_desc":
                        result = result.OrderByDescending(x => x.Price);
                        break;
                    case "price_asc":
                        result = result.OrderBy(x => x.Price);
                        break;
                    default:
                        result = result.OrderBy(x => x.Name);
                        break;
                }
            }
            if (page <= 0)
            {
                page = 1;
                return Ok(result);
            }
            else
            {
               var  result2 = PaginatedList<Product>.Create(result, page, PAGE_SIZE);
                return Ok(result2);
            }
            
            
          
         
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var productID = await _context.Products.FirstOrDefaultAsync(x=>x.Id == id);
            var result = (
              from b in _context.Products
          
           
              where b.Id == productID!.Id
              select new
              {
                  Id = b.Id,
                  name = b.Name,
            
                  Description = b.Description,
                  Content = b.Content,
                  Image = b.Image,
                  Gallery = b.Gallery,
                  Price = b.Price,
                  PriceSale = b.PriceSale,
                  Created = b.Created,
                  Updated = b.Updated,

              }
              );
            return Ok(result);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if(id == product.Id)
            {
                var productID = await _context.Products.FindAsync(id);
                if(productID != null)
                {
                    productID.Id = product.Id;
                    productID.Name = product.Name;
                    productID.Price = product.Price;
                    productID.PriceSale = product.PriceSale;
                    productID.Image=product.Image;
                    productID.Gallery=product.Gallery;
                    productID.Content = product.Content;
                    productID.Description = product.Description;
                    productID.Created = DateTime.Now;
                    productID.Updated = DateTime.Now;
                    _context.Products.Update(productID);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
               
            }
            return BadRequest();

        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            var newProdcut = new Product
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Content = product.Content,
                Description = product.Description,
                Gallery = product.Gallery,
                Image = product.Image,
                Price = product.Price,
                PriceSale = product.PriceSale,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };
            _context.Add(newProdcut);
            await _context.SaveChangesAsync();
            return Ok(newProdcut);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
