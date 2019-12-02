using Microsoft.EntityFrameworkCore;
using OrderScreen.Data;

namespace OrderScreen.Database
{
    public class CoreDbContext : DbContext
    {
        public CoreDbContext(DbContextOptions options)
			: base(options)
        {
        }

        public DbSet<Account> account {get; set;}
        public DbSet<Customer> customer {get; set;}
        public DbSet<Product> product {get; set;}
        public DbSet<Warehouse> warehouse {get; set;}
        public DbSet<Orders> orders {get; set;}
    }
}