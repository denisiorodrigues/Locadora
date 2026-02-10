using System.ComponentModel.DataAnnotations;

namespace Locadora.Api.Models;

public class Endereco
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "O campo logradouro é obrigatório")]
    public string Logradouro { get; set; }
    public int? Numero { get; set; }
}