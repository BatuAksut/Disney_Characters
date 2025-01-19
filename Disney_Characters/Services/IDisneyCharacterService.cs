using Disney_Characters.Models;

public interface IDisneyCharacterService
{
    Task<List<CharacterDto>> GetCharactersAsync();
    Task<CharacterDto> GetOneCharacterByIdAsync(int id);
    Task<CharacterDto> GetOneCharactersByNameAsync(string name);
    Task<CharacterDto> AddCharacterAsync(CharacterDto characterDto);
    public Task SeedDataFromApiAsync();
}