using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Memorize.Client.Services.Interfaces;
using Memorize.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Memorize.Client.Services.Implementations
{
    public class AuthService : ILoginService
    {
        public static User? CurrentUser { get; set; }
        
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Login(User user)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/login", itemJson);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStreamAsync();
                    var userResponse = await JsonSerializer.DeserializeAsync<User>(responseBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    CurrentUser = userResponse;
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> SignUp(User user)
        {
            try
            {
                await SetUserAvatar(user);
                var itemJson = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/users", itemJson);
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStreamAsync();
                    var userResponse = await JsonSerializer.DeserializeAsync<User>(responseBody, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    CurrentUser = userResponse;
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<User?> GetCurrentUser()
        {
            var response = await _httpClient.GetAsync($"api/login/getCurrentUser");
            var responseBody = await response.Content.ReadAsStreamAsync();
            var user = await JsonSerializer.DeserializeAsync<User>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if(!response.IsSuccessStatusCode) return null;
            return user;
        }

        public async Task<bool> Logout()
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/login/logout");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        private async Task SetUserAvatar(User user)
        {
            var path = $"{_httpClient.BaseAddress}defaultAvatar.jpg";

            user.Image = await _httpClient.GetByteArrayAsync(path);

            //    Image avatar = Image.FromFile(path);
            //byte[] imageArray;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    avatar.Save(ms, avatar.RawFormat);
            //    imageArray = ms.ToArray();
            //}
            //user.Image = imageArray;
        }
    }
}