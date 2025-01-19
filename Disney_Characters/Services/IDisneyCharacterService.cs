using Disney_Characters.Models;

public interface IDisneyCharacterService
{
    Task<List<CharacterDto>> GetCharactersAsync();
    Task<CharacterDto> GetOneCharacterByIdAsync(int id);
    Task<CharacterDto> GetOneCharacterByNameAsync(string name);
}