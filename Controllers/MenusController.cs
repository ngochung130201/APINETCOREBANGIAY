using BanGiay.Data;
using BanGiay.Models;
using BanGiay.RepositoryPattern;
using BanGiay.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanGiay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly BanGiayContext _context;
        private readonly IMenu _menuRepo;

        public MenusController(IMenu repo, BanGiayContext context)

        {
            _context = context;
            _menuRepo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMenu()
        {
           var result = await _menuRepo.getAllMenuAsync();
           return Ok(result);
     
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenu(Guid id)
        {
            try
            {
                return Ok(await _menuRepo.getMenuAsync(id));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> addNewMenu(MenuModels menu)
        {
            var newMenu = new Menu
            {
                Id = Guid.NewGuid(),
                Name = menu.Name,
                Link = menu.Link
            };
            _context.Add(newMenu);
            await _context.SaveChangesAsync();
            return Ok(newMenu);



        }
        [HttpPut("{id}")]
        public async Task<IActionResult> updateMenu(Guid id,Menu menu){
            var menuID = await _context.Menus.FindAsync(id);
            if(menuID == null){
                return NotFound();
            }
            else {
               menuID.Id = menu.Id;
               menuID.Name= menu.Name;
               menuID.Link=menu.Link;
               _context.Menus.Update(menuID);
               await _context.SaveChangesAsync();
               return Ok(menuID);
            }
        }
        // Delete 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(Guid id)
        {

          await _menuRepo.RemoveMenuAsync(id);
            return Ok();      }

        }
        
    
}
