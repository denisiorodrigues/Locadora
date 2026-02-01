using Locadora.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Api.Controllers;

[ApiController]
[Route("api/filmes")]
public class FilmeController : ControllerBase
{
    private static List<Filme> filmes = new List<Filme>();
    private static int Id = 1;
    
    [HttpPost()]
    public IActionResult Cadastrar([FromBody] Filme filme)
    {   
        filme.Id = Id++;
        filmes.Add(filme);
        return Ok();
    }
    
    [HttpGet]
    public IActionResult ObterFilmes()
    {
        return Ok(filmes);
    }
    
    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        var filme = filmes.FirstOrDefault(filme =>  filme.Id == id);
        return Ok(filme);
    }
}
