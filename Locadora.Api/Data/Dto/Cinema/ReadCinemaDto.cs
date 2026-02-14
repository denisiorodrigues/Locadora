using Locadora.Api.Data.Dto.Endereco;
using Locadora.Api.Data.Dto.Sessao;

namespace Locadora.Api.Data.Dto.Cinema;

public class ReadCinemaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public ReadEnderecoDto Endereco { get; set; }

    public IEnumerable<ReadSessaoDto> Sessoes { get; set; }
}