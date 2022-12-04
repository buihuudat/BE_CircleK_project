using CircleKAPI.Models.Product;
using CircleKAPI.Models.User;
using CircleKAPI.Models.Producer;
using Microsoft.EntityFrameworkCore;
using CircleKAPI.Models.Order;

namespace CircleKAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
