using Locadora.Api.Data.Dto.Cinema;

namespace Locadora.Api.Data.Dto.Sessao;

public class ReadSessaoDto
{
    public int Id { get; set; }
    public ReadCinemaDto Filme { get; set; }
}