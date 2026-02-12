using AutoMapper;
using Locadora.Api.Data;
using Locadora.Api.Data.Dto.Sessao;
using Locadora.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Api.Controllers;

[ApiController]
[Route("api/sessoes")]
public class SessaoController : ControllerBase
{
    private readonly LocadoraContext _context;
    private readonly IMapper _mapper;
    
    public SessaoController(LocadoraContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Cadastrar um nova sessão
    /// </summary>
    /// <param name="createSessaoDto">Objeto com os dados da sessão</param>
    /// <returns>Objeto de sessão</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Cadastrar([FromBody] CreateSessaoDto createSessaoDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        
        var sessao = _mapper.Map<Sessao>(createSessaoDto);
        _context.Sessoes.Add(sessao);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ObterPorId), new { id = sessao.Id }, sessao);
    }

    /// <summary>
    /// Obter uma msessão por id
    /// </summary>
    /// <param name="id">Identificador da sessão</param>
    /// <returns>Objeto de sessão somente leitura</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{id:int}")]
    public IActionResult ObterPorId(int id)
    {
        var consulta = _context.Sessoes.FirstOrDefault(s => s.Id == id);
        if(consulta is null) return NotFound();
        var sessao = _mapper.Map<ReadSessaoDto>(consulta);
        return Ok(sessao);
    }
    
    /// <summary>
    /// Listar as sessões
    /// </summary>
    /// <param name="pular">Quantidade para pular na paigação</param>
    /// <param name="limite">Quantidade para limitar</param>
    /// <returns>Lista de sessões</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public IActionResult Listar([FromQuery] int pular = 0 , [FromQuery] int limite = 10)
    {
        var consulta = _context.Sessoes.Skip(pular).Take(limite).ToList();
        var sessoes =  _mapper.Map<List<ReadSessaoDto>>(consulta); 
        return Ok(sessoes);
    }
}