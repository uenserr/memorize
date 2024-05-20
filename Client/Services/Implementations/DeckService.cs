using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memorize.Client.Services.Interfaces;
using System.Text;
using System.Text.Json;
using Memorize.Shared.Models;
using System.Text.Json.Serialization;

namespace Memorize.Client.Services.Implementations
{
    public class DeckService : IDeckService
    {
        private readonly HttpClient _httpClient;

        public DeckService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Card>?> GetCards(int id)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/cards/{id}");
                var cards = await JsonSerializer.DeserializeAsync<List<Card>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return cards;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<IEnumerable<Card>?> GetCardsAvailableToday(int id)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/cards/availableToday/{id}");
                var cards = await JsonSerializer.DeserializeAsync<List<Card>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return cards;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<Deck?> GetDeck(int id)
        {
            try
            {
                var response = await _httpClient.GetStreamAsync($"api/decks/deck/{id}");
                var deck = await JsonSerializer.DeserializeAsync<Deck>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return deck;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> AddCard(Card card)
        {
            try
            {
                
                var itemJson = new StringContent(JsonSerializer.Serialize(card), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/cards", itemJson);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> UpdateCard(Card card)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(card), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/cards", itemJson);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> DeleteCard(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/cards/{id}");
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


    }
}