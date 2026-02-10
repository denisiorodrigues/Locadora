using System.ComponentModel.DataAnnotations;

namespace Locadora.Api.Data.Dto.Endereco;

public class UpdateEnderecoDto
{
    [Required(ErrorMessage = "O campo logradouro é obrigatório")]
    public string Logradouro { get; set; }
    public int? Numero { get; set; }
}