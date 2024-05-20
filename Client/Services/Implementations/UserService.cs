using Memorize.Shared.Models;
using System.Text.Json;

namespace Memorize.Client.Services.Implementations
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> GetUserByLogin(string login)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/users/user/{login}");
                var user = await JsonSerializer.DeserializeAsync<User>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }
    }
}
