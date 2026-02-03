using Locadora.Api.Data;
using Locadora.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Api.Controllers;

[ApiController]
[Route("api/filmes")]
public class FilmeController : ControllerBase
{
    private LocadoraContext _context;

    public FilmeController(LocadoraContext context)
    {
        _context = context;
    }

    [HttpPost()]
    public IActionResult Cadastrar([FromBody] Filme filme)
    {   
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ObterPorId), new { id = filme.Id }, filme);
    }
    
    [HttpGet]
    public IActionResult ObterFilmes([FromQuery] int pular = 0, [FromQuery] int quantidade = 10)
    {
        return Ok(_context.Filmes.Skip(pular).Take(quantidade));
    }
    
    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme =>  filme.Id == id);
        if(filme is null) return  NotFound();
        return Ok(filme);
    }
}
