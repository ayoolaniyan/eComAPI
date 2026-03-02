using EcomAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomAPI.Data
{
    public class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; } = null!;
        
    }
}
