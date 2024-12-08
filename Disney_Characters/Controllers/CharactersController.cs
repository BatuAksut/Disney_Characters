using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Disney_Characters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public CharactersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _httpClient.GetAsync("https://api.disneyapi.dev/character");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }

            return BadRequest("API request failed");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _httpClient.GetAsync($"https://api.disneyapi.dev/character/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }

            return BadRequest("API request failed");
        }
        [HttpGet("name")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var query = $"name={name}";  
            var response = await _httpClient.GetAsync($"https://api.disneyapi.dev/character?{query}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }

            return BadRequest("API request failed");
        }


    }
}
