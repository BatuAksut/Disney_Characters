using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CharactersController : ControllerBase
{
    private readonly DisneyCharacterService _characterService;

    public CharactersController(DisneyCharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var content = await _characterService.GetAllCharactersAsync();
            return Content(content, "application/json");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var content = await _characterService.GetCharacterByIdAsync(id);
            return Content(content, "application/json");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("name")]
    public async Task<IActionResult> GetByName([FromQuery] string name)
    {
        try
        {
            var content = await _characterService.GetCharactersByNameAsync(name);
            return Content(content, "application/json");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
