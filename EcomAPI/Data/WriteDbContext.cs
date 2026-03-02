using EcomAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomAPI.Data
{
    public class WriteDbContext : DbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; } = null!;
        
    }
}
