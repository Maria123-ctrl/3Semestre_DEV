using System.ComponentModel.DataAnnotations;

namespace EventPlusTorloni.WebAPI.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = " O Email do usuário é obrigatório!")]
        public string? email { get; set; }
        [Required(ErrorMessage = " O Email do usuário é obrigatório!")]
        public string? Senha { get; set; }
        [Required(ErrorMessage = "O Tipo do usuário é obrigatório")]
        public Guid? IdTipoUsuario { get; set; }
    }
}
