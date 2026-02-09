using System.ComponentModel.DataAnnotations;

namespace Locadora.Api.Data.Dto.Cinema;

public class CreateCinemaDto
{
    [Required(ErrorMessage = "O campo nome é obrigatório")]
    public string Nome { get; set; }
}