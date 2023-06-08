using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonalProject.BLL.Entities;

namespace PersonalProject
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
    }
}