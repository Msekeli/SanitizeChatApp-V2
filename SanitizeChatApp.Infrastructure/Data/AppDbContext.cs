using Microsoft.EntityFrameworkCore;
using SanitizeChatApp.Domain.Entities;

namespace SanitizeChatApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<SensitiveWord> SensitiveWords { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
