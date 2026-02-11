using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SanitizeChatApp.Infrastructure.Data
{
    public static class WordSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.SensitiveWords.AnyAsync())
                return;

            var filePath = Path.Combine(AppContext.BaseDirectory, "sql_sensitive_list.txt");
            if (!File.Exists(filePath))
                return;

            var lines = await File.ReadAllLinesAsync(filePath);
            var words = lines
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrEmpty(line))
                .Select(word => new SanitizeChatApp.Domain.Entities.SensitiveWord { Word = word })
                .ToList();

            if (words.Count == 0)
                return;

            await context.SensitiveWords.AddRangeAsync(words);
            await context.SaveChangesAsync();
        }
    }
}