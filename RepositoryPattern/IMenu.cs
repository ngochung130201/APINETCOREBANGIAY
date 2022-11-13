using AutoMapper;
using BanGiay.Models;
using BanGiay.ViewModels;

namespace BanGiay.RepositoryPattern
{
    public interface IMenu
    {
        public Task<List<MenuModels>> getAllMenuAsync();
        public Task<Guid> AddMenuAsync(MenuModels menu);
        public Task<MenuModels> getMenuAsync(Guid id);
        public Task UpdateMenuAsync(Guid id, MenuModels menu);
        public Task RemoveMenuAsync(Guid id);
     
    }
}
