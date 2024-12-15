using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace Disney_Characters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CharactersController> _logger;

        public CharactersController(IHttpClientFactory httpClientFactory,ILogger<CharactersController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of all Disney characters.
        /// </summary>
        /// <returns>A list of Disney characters in JSON format.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Get all endpoint called. {DT}", DateTime.Now.ToLongTimeString());
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://api.disneyapi.dev/character");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            _logger.LogError("Get all request failed. {DT}", DateTime.Now.ToLongTimeString());
            return BadRequest("API request failed");
        }

        /// <summary>
        /// Retrieves a Disney character by its unique identifier.
        /// </summary>
        /// <param name="id">The unique ID of the character.</param>
        /// <returns>A Disney character in JSON format.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Get by id endpoint called. {DT}", DateTime.Now.ToLongTimeString());
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://api.disneyapi.dev/character/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            _logger.LogError("Get by id request failed. {DT}", DateTime.Now.ToLongTimeString());
            return BadRequest("API request failed");
        }

        /// <summary>
        /// Retrieves a list of Disney characters by their name.
        /// </summary>
        /// <param name="name">The name of the character to search for.</param>
        /// <returns>A list of Disney characters in JSON format that match the provided name.</returns>
        [HttpGet("name")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            _logger.LogInformation("Get by name endpoint called. {DT}", DateTime.Now.ToLongTimeString());
            var client = _httpClientFactory.CreateClient(); 
            var response = await client.GetAsync($"https://api.disneyapi.dev/character?name={name}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            _logger.LogError("Get by name request failed. {DT}", DateTime.Now.ToLongTimeString());
            return BadRequest("API request failed");
        }
    }
}
