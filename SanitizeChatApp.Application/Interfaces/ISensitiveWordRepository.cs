using System.Collections.Generic;
using System.Threading.Tasks;
using SanitizeChatApp.Domain.Entities;

namespace SanitizeChatApp.Application.Interfaces
{
	public interface ISensitiveWordRepository
	{
		Task<IEnumerable<SensitiveWord>> GetAllAsync();
		Task<SensitiveWord?> GetByIdAsync(int id);
		Task AddAsync(SensitiveWord word);
		Task UpdateAsync(SensitiveWord word);
		Task DeleteAsync(int id);
		Task<bool> ExistsAsync(int id);
	}
}

