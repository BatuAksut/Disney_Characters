using Disney_Characters;
using Disney_Characters.Models;
using Disney_Characters.Models.Disney_Characters.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

public class DisneyCharacterService : IDisneyCharacterService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<DisneyCharacterService> _logger;
    private readonly DisneyDbContext _context;

    public DisneyCharacterService(
        IHttpClientFactory httpClientFactory,
        ILogger<DisneyCharacterService> logger,
        DisneyDbContext context)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _context = context;
    }

    public async Task<List<CharacterDto>> GetCharactersAsync()
    {
        _logger.LogInformation("Fetching characters from database. {DT}", DateTime.Now.ToLongTimeString());

        var characters = await _context.Characters
            .Select(c => new CharacterDto
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl,
                SourceUrl= c.SourceUrl,
                Films = c.Films,
                TvShows=c.TvShows,

                
            })
            .ToListAsync();

        return characters;
    }

    public async Task<CharacterDto> GetOneCharacterByIdAsync(int id)
    {
        var character = await _context.Characters
            .Where(c => c.Id == id)
            .Select(c => new CharacterDto
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl,
                SourceUrl = c.SourceUrl,
                Films = c.Films,
                TvShows = c.TvShows,

            })
            .FirstOrDefaultAsync();

        if (character == null)
            throw new Exception($"Character with ID {id} not found");

        return character;
    }

    public async Task<CharacterDto> GetOneCharactersByNameAsync(string name)
    {
        var character = await _context.Characters
            .Where(c => c.Name.Contains(name))
            .Select(c => new CharacterDto
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl,
                SourceUrl = c.SourceUrl,
                Films = c.Films,
                TvShows = c.TvShows,

            })
            .FirstOrDefaultAsync();

        if (character == null)
            throw new Exception($"Character with name {name} not found");

        return character;
    }

    public async Task<CharacterDto> AddCharacterAsync(CharacterDto characterDto)
    {
        var character = new Character
        {
            Name = characterDto.Name,
            ImageUrl = characterDto.ImageUrl,
            SourceUrl = characterDto.SourceUrl,
            Films=characterDto.Films,
            TvShows=characterDto.TvShows,
            
        };

        _context.Characters.Add(character);
        await _context.SaveChangesAsync();

        characterDto.Id = character.Id;
        return characterDto;
    }


    public async Task SeedDataFromApiAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("DisneyApi");
            var response = await client.GetAsync("character");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(jsonString);
                var dataElement = jsonDocument.RootElement.GetProperty("data");
                var characters = JsonSerializer.Deserialize<List<Character>>(dataElement.GetRawText(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (characters != null)
                {
                    foreach (var character in characters)
                    {
                        character.Films ??= new List<string>();
                        character.TvShows ??= new List<string>();

                        if (!await _context.Characters.AnyAsync(c => c.Id == character.Id))
                        {
                            _logger.LogInformation($"Adding character: {character.Name} with ID: {character.Id}");
                            _context.Characters.Add(character);
                        }
                    }

                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Successfully seeded {characters.Count} characters");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while seeding data from API");
            throw;
        }
    }
}

