using AutoMapper;
using Locadora.Api.Data;
using Locadora.Api.Data.Dto.Endereco;
using Locadora.Api.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Locadora.Api.Controllers;

[ApiController]
[Route("api/enderecos")]
public class EnderecoController : ControllerBase
{
    private readonly LocadoraContext _context;
    private readonly Mapper _mapper;
    
    public EnderecoController(LocadoraContext context, Mapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Listagem de endereços
    /// </summary>
    /// <param name="pular">Quantidade que deve pular para paginação</param>
    /// <param name="limite">Quantidade que deve retornar/limitar </param>
    /// <returns>Lista de endereços</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public IActionResult Listar([FromQuery]  int pular = 0 , int limite = 10)
    {
        var consulta = _context.Enderecos.Skip(pular).Take(limite).ToList();
        var enderecosDto = _mapper.Map<List<ReadEnderecoDto>>(consulta);

        return Ok(enderecosDto);
    }
    
    /// <summary>
    /// Obter um edereço por id
    /// </summary>
    /// <param name="id">Identificador do endereço</param>
    /// <returns>Objeto com os dados de endereço encontrado</returns>
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("{id:int}")]
    public IActionResult ObterPorId(int id)
    {
        var consulta = _context.Enderecos.Find(id);
        if (consulta is null) return NotFound();
        var endereco = _mapper.Map<ReadEnderecoDto>(consulta);
        return Ok(endereco);
    }
    
    /// <summary>
    /// Cadastrar um endereço
    /// </summary>
    /// <param name="enderecoDto">Objeto com dados para cadastrar um endereço</param>
    /// <returns>Obteto de endereço criado</returns>
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost]
    public IActionResult Cadastrar([FromBody] CreateEnderecoDto enderecoDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var endereco = _mapper.Map<Endereco>(enderecoDto);
        _context.Enderecos.Add(endereco);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ObterPorId), new { Id = endereco.Id  }, endereco);
    }
    
    /// <summary>
    /// Atualizar um endereço
    /// </summary>
    /// <param name="id">Identificador do endereço a ser atualizaddo</param>
    /// <param name="enderecoDto">Objeto com os novos dados do endereço</param>
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("{id:int}")]
    public IActionResult Atualizar(int id, [FromBody] UpdateEnderecoDto enderecoDto)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var endereco = _context.Enderecos.FirstOrDefault(e => e.Id == id);
        if (endereco is null) return NotFound();
        _mapper.Map(enderecoDto, endereco);
        _context.SaveChanges();
        return NoContent();
    }
    
    /// <summary>
    /// Atualizar um endereço parcialmente, somente alguns valores
    /// </summary>
    /// <param name="id">Identificador do endereço a ser atualizaddo</param>
    /// <param name="enderecoDto">Objeto com os novos dados do endereço</param>
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPatch("{id:int}")]
    public IActionResult AtualizacaoParcial(int id, [FromBody] JsonPatchDocument<UpdateEnderecoDto> path)
    {
        var endereco = _context.Enderecos.FirstOrDefault(e => e.Id == id);
        if (endereco is null) return NotFound();
        var enderecoParaAtualizar = _mapper.Map<UpdateEnderecoDto>(endereco);
        path.ApplyTo(enderecoParaAtualizar, ModelState);
        if (!TryValidateModel(enderecoParaAtualizar)) return ValidationProblem(ModelState);
        _mapper.Map(enderecoParaAtualizar,endereco);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deletar um endereço pelo Id
    /// </summary>
    /// <param name="id">Identificador do endereço</param>
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id:int}")]
    public IActionResult Deletar(int id)
    {
        var endereco = _context.Enderecos.FirstOrDefault(e => e.Id == id);
        if (endereco is null) return NotFound();
        _context.Remove(endereco);
        _context.SaveChanges();
        return NoContent();
    }
}