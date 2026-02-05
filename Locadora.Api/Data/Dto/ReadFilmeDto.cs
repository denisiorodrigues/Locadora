using System.ComponentModel.DataAnnotations;

namespace Locadora.Api.Data.Dto;

public class ReadFilmeDto
{
    public string Titulo { get; set; }
    
    public string Genero { get; set; }
    
    public int Duracao { get; set; }

    public string Diretor { get; set; }

    public DateTime DataUltimaConsulta { get; set; } = DateTime.Now;
}