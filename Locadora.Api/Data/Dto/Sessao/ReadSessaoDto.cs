using Locadora.Api.Data.Dto.Filmes;

namespace Locadora.Api.Data.Dto.Sessao;

public class ReadSessaoDto
{
    public int FilmeId { get; set; }
    public int CinemaId { get; set; }
}