using Microsoft.AspNetCore.Mvc;
using SanitizeChatApp.Application.Interfaces;
using SanitizeChatApp.Domain.Entities;
using SanitizeChatApp.Api.Contracts;

namespace SanitizeChatApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensitiveWordsController : ControllerBase
    {
        private readonly ISensitiveWordRepository _repo;

        public SensitiveWordsController(ISensitiveWordRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensitiveWordResponse>>> GetAll()
        {
            var words = await _repo.GetAllAsync();
            return Ok(words.Select(w => new SensitiveWordResponse { Id = w.Id, Word = w.Word }));
        }

        [HttpPost]
        public async Task<ActionResult<SensitiveWordResponse>> Create([FromBody] CreateSensitiveWordRequest request)
        {
            var word = new SensitiveWord { Word = request.Word };
            await _repo.AddAsync(word);
            return CreatedAtAction(nameof(GetAll), new { id = word.Id }, new SensitiveWordResponse { Id = word.Id, Word = word.Word });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CreateSensitiveWordRequest request)
        {
            var exists = await _repo.ExistsAsync(id);
            if (!exists)
                return NotFound();
            var word = new SensitiveWord { Id = id, Word = request.Word };
            await _repo.UpdateAsync(word);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await _repo.ExistsAsync(id);
            if (!exists)
                return NotFound();
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
