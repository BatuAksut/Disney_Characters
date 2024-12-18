public class DisneyCharacterService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<DisneyCharacterService> _logger;

    public DisneyCharacterService(IHttpClientFactory httpClientFactory, ILogger<DisneyCharacterService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<string> GetAllCharactersAsync()
    {
        _logger.LogInformation("Fetching all Disney characters. {DT}", DateTime.Now.ToLongTimeString());
        var client = _httpClientFactory.CreateClient("DisneyApi");
        var response = await client.GetAsync("character");

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Successfully fetched all Disney characters. {DT}", DateTime.Now.ToLongTimeString());
            return await response.Content.ReadAsStringAsync();
        }

        _logger.LogError("Failed to fetch Disney characters. Status Code: {StatusCode}, {DT}", response.StatusCode, DateTime.Now.ToLongTimeString());
        throw new Exception("Failed to fetch Disney characters");
    }

    public async Task<string> GetCharacterByIdAsync(int id)
    {
        _logger.LogInformation("Fetching Disney character by ID: {Id}. {DT}", id, DateTime.Now.ToLongTimeString());
        var client = _httpClientFactory.CreateClient("DisneyApi");
        var response = await client.GetAsync($"character/{id}");

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Successfully fetched Disney character by ID: {Id}. {DT}", id, DateTime.Now.ToLongTimeString());
            return await response.Content.ReadAsStringAsync();
        }

        _logger.LogError("Failed to fetch Disney character by ID: {Id}. Status Code: {StatusCode}, {DT}", id, response.StatusCode, DateTime.Now.ToLongTimeString());
        throw new Exception($"Failed to fetch Disney character with ID: {id}");
    }

    public async Task<string> GetCharactersByNameAsync(string name)
    {
        _logger.LogInformation("Fetching Disney characters by name: {Name}. {DT}", name, DateTime.Now.ToLongTimeString());
        var client = _httpClientFactory.CreateClient("DisneyApi");
        var response = await client.GetAsync($"character?name={name}");

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Successfully fetched Disney characters by name: {Name}. {DT}", name, DateTime.Now.ToLongTimeString());
            return await response.Content.ReadAsStringAsync();
        }

        _logger.LogError("Failed to fetch Disney characters by name: {Name}. Status Code: {StatusCode}, {DT}", name, response.StatusCode, DateTime.Now.ToLongTimeString());
        throw new Exception($"Failed to fetch Disney characters with name: {name}");
    }
}
