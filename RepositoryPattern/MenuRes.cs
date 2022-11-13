using AutoMapper;
using BanGiay.Data;
using BanGiay.Models;
using BanGiay.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BanGiay.RepositoryPattern
{
    public class MenuRes : IMenu
    {
        private readonly BanGiayContext _context;
        private IMapper _mapper;

        public MenuRes(BanGiayContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
               
        }

        public async Task<Guid> AddMenuAsync(MenuModels menu)
        {
            var newMenu = new Menu
            {
                Id = menu.Id,
                Name = menu.Name,
                Link = menu.Link,
            };
            _context.Menus!.Add(newMenu);
            await _context.SaveChangesAsync();
            return newMenu.Id;
        }

        public async Task<List<MenuModels>> getAllMenuAsync()
        {
            var menuList = await _context.Menus.ToListAsync();
            return _mapper.Map<List<MenuModels>>(menuList);
        }

        public async Task<MenuModels> getMenuAsync(Guid id)
        {
            var menu = await _context.Menus.FindAsync(id);
            return _mapper.Map<MenuModels>(menu);
        }
        public async Task RemoveMenuAsync(Guid id)
        {
            var menu = await _context.Menus.FindAsync(id);
           
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

          

        }
      
        public async Task UpdateMenuAsync(Guid id, MenuModels menu)
        {
            if (id == menu.Id)
            {
                var updateMenu = _mapper.Map<Menu>(menu);
                _context.Menus.Update(updateMenu);
                await _context.SaveChangesAsync();
            }
        }
       

     
    }
}
