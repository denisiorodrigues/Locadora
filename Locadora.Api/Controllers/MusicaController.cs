using Microsoft.AspNetCore.Mvc;

namespace Locadora.Api.Controllers;

[ApiController]
[Route("api/musicas")]
public class MusicaController : ControllerBase
{
    private const string URL_MUSICAS = "https://guilhermeonrails.github.io/api-csharp-songs/songs.json";
    
    /// <summary>
    /// Obter a lista de musicas de uma API externa
    /// </summary>
    /// <returns code="200">Lista de musicas</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarMusicas()
    {
        using (HttpClient client = new  HttpClient())
        {
            var musicas = await client.GetStringAsync(URL_MUSICAS);
            return Ok(musicas);
        }
    }
}