using EventPlusTorloni.WebAPI.DTO;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusTorloni.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoController(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        /// <summary>
        /// Endpoint da API que faz chamada para o método de cadastrar um evento
        /// </summary>
        /// <param name="evento">Dados do evento cadastrado</param>
        /// <returns>Status code 201</returns>
        [HttpPost]
        public IActionResult Cadastrar(EventoDTO evento)
        {
            try
            {
                var novoEvento = new Evento
                {
                    IdEvento = Guid.NewGuid(),
                    Nome = evento.Nome!,
                    Descricao = evento.Descricao!,
                    DataEvento = evento.DataEvento,
                    IdTipoEvento = evento.IdTipoEvento,
                    IdInstituicao = evento.IdInstituicao
                };
                _eventoRepository.Cadastrar(novoEvento);
                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamada para o método de listar todos os eventos
        /// </summary>
        /// <returns>Status code 200 e lista com todos os eventos</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_eventoRepository.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// Endpoint da API que faz chamada para o método de atualizar um evento
        /// </summary>
        /// <param name="id">Id do evento a ser atualizado</param>
        /// <param name="evento">Dados do evento atualizado</param>
        /// <returns>Status code 204 e evento com os dados atualizados</returns>
        [HttpPut]
        public IActionResult Atualizar(Guid id, EventoDTO evento)
        {
            try
            {
                var eventoAtualizado = new Evento
                {
                    IdEvento = id,
                    Nome = evento.Nome!,
                    Descricao = evento.Descricao!,
                    DataEvento = evento.DataEvento,
                    IdTipoEvento = evento.IdTipoEvento,
                    IdInstituicao = evento.IdInstituicao
                };
                _eventoRepository.Atualizar(id, eventoAtualizado);
                return StatusCode(204, eventoAtualizado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _eventoRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que faz a chamada para o método de listar eventos filtando pelo id do usuário
        /// </summary>
        /// <param name="IdUsuario">Id do usuário para filtragem</param>
        /// <returns>Status code 200 e uma lista de eventos</returns>
        [HttpGet("Usuario/{IdUsuario}")]
        public IActionResult ListarPorId(Guid IdUsuario)
        {
            try
            {
                return Ok(_eventoRepository.ListarPorId(IdUsuario));
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }

        }

        /// <summary>
        /// Endpoint da API que faz a chamada para o método de listar os próximos eventos
        /// </summary>
        /// <returns>Status code 200 e a lista dos próximos eventos</returns>
        [HttpGet("ListarProximos")]
        public IActionResult BuscarProximosEventos()
        {
            try
            {
                return Ok(_eventoRepository.ProximosEvetos());
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
    }
}
