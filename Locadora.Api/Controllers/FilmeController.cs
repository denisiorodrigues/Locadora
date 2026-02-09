using AutoMapper;
using Locadora.Api.Data;
using Locadora.Api.Data.Dto.Filmes;
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
    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult Cadastrar([FromBody] CreateFilmeDto filmeDto)
    {   
        var filme =  _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ObterPorId), new { id = filme.Id }, filme);
    }
    
    /// <summary>
    /// Obtem uma lista de filmes do banco de dados
    /// </summary>
    /// <param name="pular">Informa a quantidade para pular a quantidade de filmes. Serve para paginação.</param>
    /// <param name="quantidade">Informa a quantidade de itens na listagem. Serve para paginação.</param>
    /// <returns code="200">Uma lista de filmes</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ObterFilmes([FromQuery] int pular = 0, [FromQuery] int quantidade = 10)
    {
        var consulta = _context.Filmes.Skip(pular).Take(quantidade);
        var filmes = _mapper.Map<List<ReadFilmeDto>>(consulta);
        return Ok(filmes);
    }
    
    /// <summary>
    /// Obtem um filme pelo seu id.
    /// </summary>
    /// <param name="id">Identifiador do filme</param>
    /// <returns code="200">Um objeto filme</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult ObterPorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme =>  filme.Id == id);
        if(filme is null) return  NotFound();
        
        return Ok(_mapper.Map<ReadFilmeDto>(filme));
    }
    
    /// <summary>
    /// Atualiza um filme no banco de dados.
    /// </summary>
    /// <param name="id">Identificador do filme</param>
    /// <param name="filmeDto">Objeto com os campos necessários para atualização.</param>
    /// <returns code="204">Não há retorno</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
        if(filme is null) return NotFound();
        filme = _mapper.Map (filmeDto, filme);        
        _context.SaveChanges();
        return NoContent();
    }
    
    /// <summary>
    /// Atualização parcial de uma filme
    /// </summary>
    /// <param name="id">identificadoor do filme</param>
    /// <param name="path">Objeto json path</param>
    /// <returns code="204">Não há retorno</returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
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
    
    /// <summary>
    /// Remover um filme do banco de dados
    /// </summary>
    /// <param name="id">identificador de um filme</param>
    /// <returns code="204">Não há retorno</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeletarFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(f => f.Id == id);
        if(filme is null) return NotFound();
        _context.Filmes.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
