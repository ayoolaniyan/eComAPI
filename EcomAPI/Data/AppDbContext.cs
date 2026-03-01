using EcomAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; } = null!;
        
    }
}
