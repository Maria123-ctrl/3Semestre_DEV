using System.ComponentModel.DataAnnotations;

namespace EventPlusTorloni.WebAPI.DTO
{
    public class InstituicaoDTO
    {
        //[Required(ErrorMessage = "O NomeFantasia da Instituição é obrigatório!")]
        public string? NomeFantasia { get; set; }

        //[Required(ErrorMessage = "O CNPJ da Instituição é obrigatório!")]
        public string? Cnpj { get; set; }

        //[Required(ErrorMessage = "O Endereco da Instituição é obrigatório!")]
        public string? Endereco { get; set; }
    }
}
