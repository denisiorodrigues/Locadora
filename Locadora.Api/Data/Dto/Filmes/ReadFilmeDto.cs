using Locadora.Api.Data.Dto.Sessao;

namespace Locadora.Api.Data.Dto.Filmes;

public class ReadFilmeDto
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public int Duracao { get; set; }
    public string Diretor { get; set; }
    public DateTime DataUltimaConsulta { get; set; } = DateTime.Now;
    public IEnumerable<ReadSessaoDto> Sessoes { get; set; }
}