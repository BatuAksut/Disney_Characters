using DataAccess.Abstract;
using Disney_Characters.Models;
using System.Text.Json;

public class DisneyCharacterService : IDisneyCharacterService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<DisneyCharacterService> _logger;
    private readonly ICharacterRepository _characterRepository;

    public DisneyCharacterService(IHttpClientFactory httpClientFactory, ILogger<DisneyCharacterService> logger, ICharacterRepository characterRepository)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _characterRepository = characterRepository;
    }

    public async Task<List<CharacterDto>> GetCharactersAsync()
    {
        _logger.LogInformation("Fetching all Disney characters. {DT}", DateTime.Now.ToLongTimeString());
        var client = _httpClientFactory.CreateClient("DisneyApi");
        var response = await client.GetAsync("character");

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Received JSON response: {JsonResponse}", jsonString);

            var jsonDocument = JsonDocument.Parse(jsonString);
            var dataElement = jsonDocument.RootElement.GetProperty("data");

            if (dataElement.ValueKind == JsonValueKind.Array && dataElement.GetArrayLength() > 0)
            {
                _logger.LogInformation("JSON data found, deserializing the characters.");
                var characters = new List<CharacterDto>();

                foreach (var item in dataElement.EnumerateArray())
                {
                    // Veriyi manuel olarak eşliyoruz
                    var character = new CharacterDto
                    {
                        Id = item.GetProperty("_id").GetInt32(),
                        Name = item.GetProperty("name").GetString(),
                        Films = item.TryGetProperty("films", out var films) ? films.EnumerateArray().Select(f => f.GetString()).ToList() : new List<string>(), // Boş liste yerine []
                        TvShows = item.TryGetProperty("tvShows", out var tvShows) ? tvShows.EnumerateArray().Select(t => t.GetString()).ToList() : new List<string>(), // Boş liste yerine []
                        ImageUrl = item.GetProperty("imageUrl").GetString(),
                        SourceUrl = item.GetProperty("sourceUrl").GetString()
                    };

                    // Veritabanında bu isme sahip bir karakter var mı?
                    var existingCharacter = _characterRepository.Get(c => c.Name == character.Name);
                    if (existingCharacter == null) // Eğer yoksa, ekle
                    {
                        _logger.LogInformation("Adding new character: {CharacterName}", character.Name);
                        // Veritabanına ekle
                        _characterRepository.Add(character);
                    }
                    else
                    {
                        _logger.LogInformation("Character {CharacterName} already exists in the database.", character.Name);
                    }

                    characters.Add(character);
                }

                // Karakterleri döndür
                return characters;
            }
            else
            {
                _logger.LogError("No characters found in the JSON data.");
                throw new Exception("No characters found in the response data.");
            }
        }

        _logger.LogError("Failed to fetch Disney characters. Status Code: {StatusCode}, {DT}", response.StatusCode, DateTime.Now.ToLongTimeString());
        throw new Exception("Failed to fetch Disney characters");
    }






    public async Task<CharacterDto> GetOneCharacterByIdAsync(int id)
    {
        _logger.LogInformation("Fetching Disney character by ID: {Id}. {DT}", id, DateTime.Now.ToLongTimeString());
        var client = _httpClientFactory.CreateClient("DisneyApi");
        var response = await client.GetAsync($"character/{id}");

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Received JSON response: {JsonString}", jsonString.Substring(0, Math.Min(200, jsonString.Length))); // İlk 200 karakteri logla

            var jsonDocument = JsonDocument.Parse(jsonString);
            var dataElement = jsonDocument.RootElement.GetProperty("data");
            var character = JsonSerializer.Deserialize<CharacterDto>(dataElement.GetRawText(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            _logger.LogInformation("Successfully fetched Disney character by ID: {Id}. {DT}", id, DateTime.Now.ToLongTimeString());
            return character;
        }

        _logger.LogError("Failed to fetch Disney character by ID: {Id}. Status Code: {StatusCode}, {DT}", id, response.StatusCode, DateTime.Now.ToLongTimeString());
        throw new Exception($"Failed to fetch Disney character with ID: {id}");
    }

    public async Task<CharacterDto> GetOneCharacterByNameAsync(string name)
    {
        _logger.LogInformation("Fetching Disney characters by name: {Name}. {DT}", name, DateTime.Now.ToLongTimeString());
        var client = _httpClientFactory.CreateClient("DisneyApi");
        var response = await client.GetAsync($"character?name={name}");

        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Received JSON response: {JsonString}", jsonString.Substring(0, Math.Min(200, jsonString.Length))); // İlk 200 karakteri logla

            var jsonDocument = JsonDocument.Parse(jsonString);
            var dataElement = jsonDocument.RootElement.GetProperty("data");
            var character = JsonSerializer.Deserialize<CharacterDto>(dataElement.GetRawText(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            _logger.LogInformation("Successfully fetched Disney characters by name: {Name}. {DT}", name, DateTime.Now.ToLongTimeString());
            return character;
        }

        _logger.LogError("Failed to fetch Disney characters by name: {Name}. Status Code: {StatusCode}, {DT}", name, response.StatusCode, DateTime.Now.ToLongTimeString());
        throw new Exception($"Failed to fetch Disney characters with name: {name}");
    }
}
