using System.ComponentModel.DataAnnotations;

namespace EventPlusTorloni.WebAPI.DTO
{
    public class TipoUsuarioDTO
    {
        [Required(ErrorMessage = "O titulo do tipo de evento é obrigatório!")]
        public string? Titulo { get; set; }
    }
}
