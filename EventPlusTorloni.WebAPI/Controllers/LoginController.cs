using EventPlusTorloni.WebAPI.DTO;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EventPlusTorloni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuariorRepository _usuarioRepository;

        public LoginController(IUsuariorRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                if(loginDTO.IdTipoUsuario == null)
                {
                    return BadRequest("IdTipousuario é obrigatório.");
                }
                Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDTO.email!, loginDTO.Senha!, loginDTO.IdTipoUsuario.Value);

                if (usuarioBuscado == null)
                {
                    return NotFound("Email ou Senha inválidos!");
                }

                //Caso encontre o usuário, prosseguir para criação do token

                //1 - Definir as informações(Claims) que serão fornecidas no token (Payload)
                var Claims = new[]
                {
                    //formato da claim
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),

                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!),

                    new Claim(JwtRegisteredClaimNames.Name, usuarioBuscado.Nome!),

                    new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuarioNavigation!.Titulo!),

                    //existe a possibilidade de criar uma claim personalizada
                    //new Claim("Claim Personalizada", "Valor da claim personalizada")
                };

                //2 - definir a chave de acesso ao token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("eventplus-chave-autenticacao-webapi-dev"));

                //3 - Definir as credencias do token (HEADER)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //4 - Gerar token
                var token = new JwtSecurityToken
                (
                    //emissor do token
                    issuer: "api_eventplus",

                    //destinatário do token
                    audience: "api_eventplus",

                    //dados definidos nas claims(informações)
                    claims: Claims,

                    //tempo de expiração do token
                    expires: DateTime.Now.AddMinutes(5),

                    //credenciais do token
                    signingCredentials: creds
                );

                //5 - Retornar o token criado
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });


            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }
    }
}

