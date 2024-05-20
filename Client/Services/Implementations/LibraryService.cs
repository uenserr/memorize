using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memorize.Client.Services.Interfaces;
using System.Text;
using System.Text.Json;
using Memorize.Shared.Models;
using System.Text.Json.Serialization;
using System.Runtime.InteropServices;

namespace Memorize.Client.Services.Implementations
{
    public class LibraryService : ILibraryService
    {
        private readonly HttpClient _httpClient;

        public LibraryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Deck>?> GetCurrentUserDecks(User user)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/decks/userDecks/{user.ID}");
                var responseBody = await response.Content.ReadAsStreamAsync();
                var decks = await JsonSerializer.DeserializeAsync<List<Deck>>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                });
                return decks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> AddDeck(Deck deck)
        {
            try
            {
                deck.Name = "New deck";
                var itemJson = new StringContent(JsonSerializer.Serialize(deck), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/decks", itemJson);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> UpdateDeck(Deck deck)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(deck), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/decks", itemJson);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> DeleteDeck(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/decks/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

    }
}