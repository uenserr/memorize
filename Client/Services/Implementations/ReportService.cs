using Memorize.Shared.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;

namespace Memorize.Client.Services.Implementations
{
    public class ReportService
    {
        private readonly HttpClient _httpClient;

        public ReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Report>?> GetAllReports()
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/reports");
                var responseBody = await response.Content.ReadAsStreamAsync();
                var reports = await JsonSerializer.DeserializeAsync<List<Report>>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                });
                return reports;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }


        public async Task<Report?> AddReport(Report report)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(report), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/reports", itemJson);
                var responseContent = await response.Content.ReadAsStreamAsync();
                var responseReport = await JsonSerializer.DeserializeAsync<Report>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return responseReport;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> UpdateReport(Report report)
        {
            try
            {
                var itemJson = new StringContent(JsonSerializer.Serialize(report), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/reports", itemJson);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<bool> DeleteReport(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/reports/{id}");
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
