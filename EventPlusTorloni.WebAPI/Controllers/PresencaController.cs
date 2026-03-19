using EventPlusTorloni.WebAPI.DTO;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using EventPlusTorloni.WebAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusTorloni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresencaController : ControllerBase
    {
        private IPresencaRepository _presencaRepository;

        public PresencaController(IPresencaRepository presencaRepository)
        {
            _presencaRepository = presencaRepository;
        }


        
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_presencaRepository.Listar());

            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz a chamada para o método de cadastrar um tipo de instituicao
        /// </summary>
        /// <param name="tipoEvento">Tipo de instituicao a ser cadastrado</param>
        /// <returns>Status code 201 e o tipo de instituicao cadastrado</returns>
        [HttpPost]
        public IActionResult Inscrever(PresencaDTO presenca)
        {
            try
            {
                var novaPresenca = new Presenca
                {
                    Situacao = presenca.Situacao!,
                    IdEvento = presenca.IdEvento!,
                    IdUsuario = presenca.IdUsuario!
                };

                _presencaRepository.Inscrever(novaPresenca);

                return StatusCode(201, novaPresenca);
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
        public IActionResult Atualizar(Guid id, PresencaDTO presenca)
        {
            try
            {
                var presencaAtualizada = new Presenca
                {
                    IdPresenca =Guid.NewGuid(),
                    Situacao = presenca.Situacao!,
                    IdEvento = presenca.IdEvento!,
                    IdUsuario = presenca.IdUsuario!
                };

                _presencaRepository.Atualizar(id, presencaAtualizada);

                return StatusCode(204, presencaAtualizada);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que retorna uma presença por id
        /// </summary>
        /// <param name="id">id da presença a ser buscada</param>
        /// <returns>Status code 200 e presença</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_presencaRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que retorna uma lista de presenças filtrada por usuário
        /// </summary>
        /// <param name="idUsuario">id do uuário para filtragem</param>
        /// <returns>uma lista de presenças filtrada pelo usuário</returns>
        [HttpGet("ListarMinhas/{idUsuario}")]
        public IActionResult BuscarPorUsuario(Guid idUsuario)
        {
            try
            {
                return Ok(_presencaRepository.ListarMinhas(idUsuario));
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz a chamada para o método de Deletar um tipo de evento
        /// </summary>
        /// <param name="id">Id do tipo do evento a ser excluído</param>
        /// <returns>Status Code 204</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _presencaRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
    }
}

