using SanitizeChatApp.Client.Models;       // Imports shared models for DTOs
using System.Net.Http.Json;                // Enables simplified JSON HTTP calls

namespace SanitizeChatApp.Client.Services
{
    // Service class that handles all HTTP communication with the backend API
    public class WordService
    {
        private readonly HttpClient _http;

        // HttpClient is injected via DI and used to make API calls
        public WordService(HttpClient http)
        {
            _http = http;
        }

        // ================================
        // Admin - CRUD for Sensitive Words
        // ================================

        // Fetches all sensitive words from the backend
        public async Task<List<SensitiveWord>> GetWordsAsync()
        {
            return await _http.GetFromJsonAsync<List<SensitiveWord>>("api/words") ?? new List<SensitiveWord>();
        }

        // Adds a new sensitive word to the backend
        public async Task AddWordAsync(SensitiveWord word)
        {
            await _http.PostAsJsonAsync("api/words", word);
        }

        // Updates an existing sensitive word
        public async Task UpdateWordAsync(SensitiveWord word)
        {
            await _http.PutAsJsonAsync($"api/words/{word.Id}", word);
        }

        // Deletes a sensitive word by ID
        public async Task DeleteWordAsync(int id)
        {
            await _http.DeleteAsync($"api/words/{id}");
        }

        // ================================
        // Chat - Sanitization
        // ================================

        // Sends a message to the backend for sanitization and returns the sanitized result
        public async Task<string> SanitizeMessageAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return string.Empty;

            var request = new MessageRequest { Message = message };
            var response = await _http.PostAsJsonAsync("api/sanitization", request);

            if (!response.IsSuccessStatusCode)
                return message;

            var result = await response.Content.ReadFromJsonAsync<SanitizedResponse>();
            return result?.SanitizedMessage ?? message;
        }
    }

    // DTO for sending a message to the API
    public class MessageRequest
    {
        public string Message { get; set; } = string.Empty;
    }

    // DTO for receiving the sanitized message from the API
    public class SanitizedResponse
    {
        public string SanitizedMessage { get; set; } = string.Empty;
    }
}
