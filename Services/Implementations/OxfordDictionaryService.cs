using System;
using System.Collections.Generic;
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
            _baseUrl = "https://api.dictionaryapi.dev/api/v2";
        }

        public async Task<OxfordDictionaryDto> GetWordDefinitionAsync(string word)
        {
            var url = $"{_baseUrl}/entries/en/{word.ToLower()}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Oxford API trả về lỗi: {response.StatusCode}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonElements = JsonDocument.Parse(jsonResponse).RootElement;

            var result = new OxfordDictionaryDto
            {
                Word = jsonElements[0].GetProperty("word").GetString(),
                Phonetic = jsonElements[0].TryGetProperty("phonetic", out var phonetic) ? phonetic.GetString() : "",
                Phonetics = new List<OxfordDictionaryDto.PhoneticDTO>(),
                Meanings = new List<OxfordDictionaryDto.MeaningDTO>()
            };

            if (jsonElements[0].TryGetProperty("phonetics", out var phonetics))
            {
                foreach (var item in phonetics.EnumerateArray())
                {
                    result.Phonetics.Add(new OxfordDictionaryDto.PhoneticDTO
                    {
                        Text = item.TryGetProperty("text", out var text) ? text.GetString() : "",
                        Audio = item.TryGetProperty("audio", out var audio) ? audio.GetString() : ""
                    });
                }
            }

            if (jsonElements[0].TryGetProperty("meanings", out var meanings))
            {
                foreach (var meaning in meanings.EnumerateArray())
                {
                    var meaningDto = new OxfordDictionaryDto.MeaningDTO
                    {
                        PartOfSpeech = meaning.GetProperty("partOfSpeech").GetString(),
                        Definitions = new List<OxfordDictionaryDto.DefinitionDTO>(),
                        Synonyms = new List<string>(),
                        Antonyms = new List<string>()
                    };

                    foreach (var definition in meaning.GetProperty("definitions").EnumerateArray())
                    {
                        meaningDto.Definitions.Add(new OxfordDictionaryDto.DefinitionDTO
                        {
                            Definition = definition.GetProperty("definition").GetString(),
                            Example = definition.TryGetProperty("example", out var example) ? example.GetString() : "",
                            Synonyms = definition.TryGetProperty("synonyms", out var synonyms)
                                ? JsonSerializer.Deserialize<List<string>>(synonyms.GetRawText())
                                : new List<string>(),
                            Antonyms = definition.TryGetProperty("antonyms", out var antonyms)
                                ? JsonSerializer.Deserialize<List<string>>(antonyms.GetRawText())
                                : new List<string>()
                        });
                    }

                    meaningDto.Synonyms = meaning.TryGetProperty("synonyms", out var mainSynonyms)
                        ? JsonSerializer.Deserialize<List<string>>(mainSynonyms.GetRawText())
                        : new List<string>();

                    meaningDto.Antonyms = meaning.TryGetProperty("antonyms", out var mainAntonyms)
                        ? JsonSerializer.Deserialize<List<string>>(mainAntonyms.GetRawText())
                        : new List<string>();

                    result.Meanings.Add(meaningDto);
                }
            }

            return result;
        }
    }
}
