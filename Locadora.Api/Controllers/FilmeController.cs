using Microsoft.AspNetCore.Mvc;

namespace Locadora.Api.Controllers;

[ApiController]
[Route("api/filmes")]
public class FilmeController : ControllerBase
{
    [HttpPost()]
    public IActionResult Cadastrar()
    {
        return Ok();
    }
}
