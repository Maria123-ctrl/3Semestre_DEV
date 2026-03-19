using EventPlusTorloni.WebAPI.DTO;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusTorloni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuariorRepository _usuariorRepository;

        public UsuarioController(IUsuariorRepository usuariorRepository)
        {
            _usuariorRepository = usuariorRepository;
        }

        /// <summary>
        /// Endpoint da API que faz a chamada para o método de Buscar um usuário por id
        /// </summary>
        /// <returns>id do usuário a ser buscado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_usuariorRepository.BuscarPorId(id));
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }



        /// <summary>
        /// Endpoint da API que faz a chamada para o método cadastrar
        /// </summary>
        /// <param name="usuario">Usuário a ser cadastrado</param>
        /// <returns>StatusCode 201 e o usuário cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(UsuarioDTO usuario)
        {
            try
            {
                var usuarioDTO = new Usuario()
                {
                    IdUsuario = Guid.NewGuid(),
                    Nome = usuario.Nome!,
                    Email = usuario.Email!,
                    Senha = usuario.Senha!,
                    IdTipoUsuario = usuario.IdTipoUsuario,
                };
                _usuariorRepository.Cadastrar(usuarioDTO);

                return StatusCode(201, usuario);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }
        }
    }
}
