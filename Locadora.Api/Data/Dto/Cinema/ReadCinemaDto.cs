using Locadora.Api.Data.Dto.Endereco;

namespace Locadora.Api.Data.Dto.Cinema;

public class ReadCinemaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public ReadEnderecoDto Endereco { get; set; }
}