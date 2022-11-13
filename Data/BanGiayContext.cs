using BanGiay.Models;
using Microsoft.EntityFrameworkCore;

namespace BanGiay.Data
{
    public class BanGiayContext:DbContext
    {
        public BanGiayContext(DbContextOptions<BanGiayContext> options):base(options)
        {
                
        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get;set; }

    }
}
