using Memorize.Shared.Models;
using System.Text.Json;

namespace Memorize.Client.Services.Implementations
{
    public class SearchService
    {
        private readonly HttpClient _httpClient;

        public SearchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Deck?>> GetDecksByName(string query)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/decks/param/{query}");
                var decks = await JsonSerializer.DeserializeAsync<List<Deck>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return decks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<List<User?>> GetUsersByName(string query)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/users/param/{query}");
                var users = await JsonSerializer.DeserializeAsync<List<User>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }
    }
}
