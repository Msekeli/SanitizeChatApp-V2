using Microsoft.EntityFrameworkCore;
using SanitizeChatApp.Application.Interfaces;
using SanitizeChatApp.Domain.Entities;
using SanitizeChatApp.Infrastructure.Data;

namespace SanitizeChatApp.Infrastructure.Repositories
{
    public class SensitiveWordRepository : ISensitiveWordRepository
    {
        private readonly AppDbContext _context;

        public SensitiveWordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SensitiveWord>> GetAllAsync()
        {
            return await _context.SensitiveWords.ToListAsync();
        }

        public async Task<SensitiveWord?> GetByIdAsync(int id)
        {
            return await _context.SensitiveWords.FindAsync(id);
        }

        public async Task AddAsync(SensitiveWord word)
        {
            await _context.SensitiveWords.AddAsync(word);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SensitiveWord word)
        {
            _context.SensitiveWords.Update(word);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.SensitiveWords.FindAsync(id);
            if (entity != null)
            {
                _context.SensitiveWords.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.SensitiveWords.AnyAsync(w => w.Id == id);
        }
    }
}
