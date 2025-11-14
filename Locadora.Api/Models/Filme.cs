using System.ComponentModel.DataAnnotations;

namespace Locadora.Api.Models;

public class Filme : EntidadeBase
{
    [Required(ErrorMessage = "O título deve ser informado.")]
    [MaxLength(50, ErrorMessage = "O título deve ter no máximo 50 caracteres.")]
    public string Titulo { get; set; }
    
    [Required(ErrorMessage = "O gênero deve ser informado.")]
    public Genero Genero { get; set; }
    
    [Required(ErrorMessage = "A duração deve ser informada.")]
    [Range(1, 360, ErrorMessage = "A duração deve ter no mínimo 1 minuto e no máximo 360")]
    public int Duracao { get; set; }

    [MaxLength(100, ErrorMessage = "O nome do diretor deve ter no máximo 100 caracteres.")]
    public string Diretor { get; set; }
}
