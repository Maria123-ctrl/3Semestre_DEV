using System.ComponentModel.DataAnnotations;

namespace FilmesTorloni.WebAPI.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = " O Email do usuário é obrigatório!")]
        public string? email { get; set; }
        [Required(ErrorMessage = " O Email do usuário é obrigatório!")]
        public string? Senha { get; set; }
    }
}
