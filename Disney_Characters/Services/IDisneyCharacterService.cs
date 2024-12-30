public interface IDisneyCharacterService
{
    Task<string> GetCharactersAsync();
    Task<string> GetOneCharacterByIdAsync(int id);
    Task<string> GetOneCharactersByNameAsync(string name);
}