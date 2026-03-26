using System.ComponentModel.DataAnnotations;

namespace ConnectPlus.DTO
{
    public class TipoContatoDTO
    {
        [Required(ErrorMessage = "O titulo do tipo de evento é obrigatório!")]
        public string? Titulo { get; set; }
    }
}
