using AutoMapper;
using Locadora.Api.Data;
using Locadora.Api.Data.Dto;
using Locadora.Api.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Api.Controllers;

[ApiController]
[Route("api/filmes")]
public class FilmeController : ControllerBase
{
    private LocadoraContext _context;
    private IMapper _mapper;

    public FilmeController(LocadoraContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost()]
    public IActionResult Cadastrar([FromBody] CreateFilmeDto filmeDto)
    {   
        var filme =  _mapper.Map<Filme>(filmeDto);
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

    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
        if(filme is null) return NotFound();
        filme = _mapper.Map (filmeDto, filme);        
        // _context.Filmes.Update(filme);
        _context.SaveChanges();
        return NoContent();
    }
    
    [HttpPatch("{id}")]
    public IActionResult AtualizarFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> path)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
        if(filme is null) return NotFound();
        
        var filmeParaAtualziar = _mapper.Map<UpdateFilmeDto>(filme);
        
        path.ApplyTo(filmeParaAtualziar, ModelState);
        if (!TryValidateModel(filmeParaAtualziar))
        {
            return ValidationProblem(ModelState);
        }
        
        _mapper.Map(filmeParaAtualziar, filme);
        _context.SaveChanges();
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeletarFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
        if(filme is null) return NotFound();
        _context.Filmes.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
