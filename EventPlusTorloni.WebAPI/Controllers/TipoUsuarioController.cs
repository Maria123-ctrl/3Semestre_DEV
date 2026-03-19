using EventPlusTorloni.WebAPI.DTO;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using EventPlusTorloni.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusTorloni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private ITipoUsuarioRepository _tipoUsuarioRepository;

        public TipoUsuarioController(ITipoUsuarioRepository tipoUsuarioRepository)
        {
            _tipoUsuarioRepository = tipoUsuarioRepository;
        }



        /// <summary>
        /// Endpoint da API que faz chamada para o método de listar os tipos de evento
        /// </summary>
        /// <returns>Status code 200 e a lista de tipos de evento</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_tipoUsuarioRepository.Listar());

            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que faz a chamada para um método de busca de um tipo de evento específico
        /// </summary>
        /// <param name="id">Id do tipo de evento buscado</param>
        /// <returns>Status code 200 e tipo de evento buscado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_tipoUsuarioRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }


        /// <summary>
        /// Endpoint da API que faz a chamada para o método de cadastrar um tipo de usuario
        /// </summary>
        /// <param name="tipoEvento">Tipo de usuario a ser cadastrado</param>
        /// <returns>Status code 201 e o tipo de usuario cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(TipoUsuarioDTO tipoUsuario)
        {
            try
            {
                var novoTipoUsuario = new TipoUsuario
                {
                    Titulo = tipoUsuario.Titulo!
                };

                _tipoUsuarioRepository.Cadastrar(novoTipoUsuario);

                return StatusCode(201, novoTipoUsuario);
            }
            catch (Exception erro)
            {


                return BadRequest(erro.Message);
            }
        }


        /// <summary>
        /// Endpoint da API que faz a chamada para o método de Atualizar um tipo de evento
        /// </summary>
        /// <param name="id">Id do tipo de evento a ser atualizado</param>
        /// <param name="tipoEvento">Tipo de evento com os dados atualizados</param>
        /// <returns>Status code 204 e o tipo de evento atualizado</returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, TipoUsuarioDTO tipoUsuario)
        {
            try
            {
                var tipoUsuarioAtualizado = new TipoUsuario
                {
                    Titulo = tipoUsuario.Titulo!
                };

                _tipoUsuarioRepository.Atualizar(id, tipoUsuarioAtualizado);

                return StatusCode(204, tipoUsuarioAtualizado);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz a chamada para o método de Deletar um tipo de usuario
        /// </summary>
        /// <param name="id">Id do tipo do evento a ser excluído</param>
        /// <returns>Status Code 204</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _tipoUsuarioRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
    }
}
