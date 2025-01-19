using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CharactersController : ControllerBase
{
    private readonly IDisneyCharacterService _disneyCharacterService;

    public CharactersController(IDisneyCharacterService disneyCharacterService)
    {
        _disneyCharacterService = disneyCharacterService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var content = await _disneyCharacterService.GetCharactersAsync();
            return Ok(content);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne([FromRoute] int id)
    {
        try
        {
            var content = await _disneyCharacterService.GetOneCharacterByIdAsync(id);
            return Ok(content);
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
            var result = await _disneyCharacterService.GetOneCharacterByNameAsync(name);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
