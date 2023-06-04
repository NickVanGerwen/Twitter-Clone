using Microsoft.EntityFrameworkCore;

namespace twitter_fetch_service.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Post> Posts { get; set; }
    }
}
