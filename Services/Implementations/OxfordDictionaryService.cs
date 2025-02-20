using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DotnetAPIProject.Services.Implementations
{
    public class OxfordDictionaryService : IOxfordDictionaryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public OxfordDictionaryService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["OxfordDictionary:BaseUrl"] ?? "https://api.dictionaryapi.dev/api/v2";

            if (string.IsNullOrEmpty(_baseUrl))
            {
                throw new ArgumentException("Lỗi cấu hình: BaseUrl không hợp lệ!");
            }

            _httpClient.Timeout = TimeSpan.FromSeconds(200);
        }


        public async Task<string> GetWordDefinitionAsync(string word)
        {
            var url = $"{_baseUrl}/entries/en/{word.ToLower()}";
            Console.WriteLine($"Gọi API Oxford: {url}");

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Oxford API trả về lỗi: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(jsonResponse);
            return doc.RootElement.ToString();
        }

    }
}
