using AutoMapper;
using Locadora.Api.Data;
using Locadora.Api.Data.Dto.Cinema;
using Locadora.Api.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Api.Controllers;

[ApiController]
[Route("api/cinema")]
public class CinemaController : ControllerBase
{
    private readonly Mapper _mapper;
    private readonly LocadoraContext _context;
    
    public CinemaController(Mapper mapper, LocadoraContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    /// <summary>
    /// Listar todos os cinemas paginados
    /// </summary>
    /// <param name="pular">Quantidade que tem que pular</param>
    /// <param name="quantidade">Quantidade que tem que trazer</param>
    /// <returns>Lista de cinemas</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public IActionResult Listar([FromQuery] int pular = 0, int quantidade = 10)
    {
        var cinemas = _context.Cinemas.Skip(pular).Take(quantidade).ToList();
        return Ok(cinemas);
    }
    
    /// <summary>
    /// Cadastrar um cinema
    /// </summary>
    /// <param name="createCinemaDto">Objeto com os dados para criar um cinema</param>
    /// <returns>Objeto criado</returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost]
    public IActionResult Cadastrar([FromBody] CreateCinemaDto createCinemaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cinema = _mapper.Map<Cinema>(createCinemaDto);
        
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        
        return CreatedAtAction(nameof(ObterPorId), new { id = cinema.Id }, cinema);
    }
    
    /// <summary>
    /// Obter um cinema por id
    /// </summary>
    /// <param name="id">Identifficador do cinema</param>
    /// <returns>O Cinema encontrado</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var cinema = _context.Cinemas.FirstOrDefault(c => c.Id == id);
        if (cinema is null)
        {
            return NotFound();
        }
        
        var cinemaLeitura = _mapper.Map<ReadCinemaDto>(cinema);
        
        return Ok(cinemaLeitura);
    }
    
    /// <summary>
    /// Atualizar o cinema
    /// </summary>
    /// <param name="id">Identificador do cinema</param>
    /// <param name="updateCinemaDto">Objeto com os dados para atualizar o cinema</param>
    /// <returns>Sem Retorno</returns>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("{id}")]
    public IActionResult Atualizar([FromQuery] int id, [FromBody] UpdateCinemaDto updateCinemaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cinema = _context.Cinemas.FirstOrDefault(c => c.Id == id);

        if (cinema is null)
        {
            return NotFound();
        }
        
        _mapper.Map(updateCinemaDto, cinema);
        _context.SaveChanges();
        
        return NoContent();
    }
    
    /// <summary>
    /// Atualição parcial de uma cinema
    /// </summary>
    /// <param name="id">Identificador do cinema</param>
    /// <param name="path">Parth de dados que precisa sem atualizado</param>
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPatch("{id}")]
    public IActionResult AtualizacaoParcial(int id, JsonPatchDocument<UpdateCinemaDto> path)
    {
        var cinema = _context.Cinemas.FirstOrDefault(c => c.Id == id);
        if (cinema is null)
        {
            return NotFound();
        }
        
        var cinemaParaAtualizar = _mapper.Map<UpdateCinemaDto>(cinema);
        
        path.ApplyTo(cinemaParaAtualizar, ModelState);
        if (!TryValidateModel(cinemaParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(cinemaParaAtualizar, cinema);
        _context.SaveChanges();
        return NoContent();
    }
    
    /// <summary>
    /// Deletar um cinema
    /// </summary>
    /// <param name="id">IDentificador de um cinema</param>
    /// <returns>Sem retorno</returns>
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
        var cinema = _context.Cinemas.FirstOrDefault(c => c.Id == id);
        if (cinema is null)
        {
            return NotFound();
        }

        _context.Cinemas.Remove(cinema);
        _context.SaveChanges();
        return NoContent();
    }
}