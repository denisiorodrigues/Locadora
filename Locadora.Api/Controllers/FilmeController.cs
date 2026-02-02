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
        return CreatedAtAction(nameof(ObterPorId), new { id = filme.Id }, filme);
    }
    
    [HttpGet]
    public IActionResult ObterFilmes([FromQuery] int pular = 0, [FromQuery] int quantidade = 10)
    {
        return Ok(filmes.Skip(pular).Take(quantidade));
    }
    
    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        var filme = filmes.FirstOrDefault(filme =>  filme.Id == id);
        if(filme is null) return  NotFound();
        return Ok(filme);
    }
}
