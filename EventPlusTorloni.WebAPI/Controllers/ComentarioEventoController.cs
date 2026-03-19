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
    public class ComentarioEventoController : ControllerBase
    {
        private readonly IComentarioEventoRepository _comentarioEventoRepository;

        public ComentarioEventoController(IComentarioEventoRepository comentarioEventoRepository)
        {
           _comentarioEventoRepository = comentarioEventoRepository;
        }

        /// <summary>
        /// Endpoint da API que faz chamada para o método de cadastrar um evento
        /// </summary>
        /// <param name="evento">Dados do evento cadastrado</param>
        /// <returns>Status code 201</returns>
        [HttpPost]
        public IActionResult Cadastrar(ComentarioEventoDTO comentarioEvento)
        {
            try
            {
                var novoComentarioEvento = new ComentarioEvento

                {
                    IdComentarioEvento = Guid.NewGuid(),
                    Descricao = comentarioEvento.Descricao!,
                    Exibe = comentarioEvento.Exibe!.Value,
                    DataComentarioEvento = comentarioEvento.DataComentrioEvento,
                    IdEvento = comentarioEvento.IdEvento,
                    IdUsuario = comentarioEvento.IdUsuario!
                };
                _comentarioEventoRepository.Cadastrar(novoComentarioEvento);
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
        public IActionResult Listar(Guid IdEvento)
        {
            try
            {
                return Ok(_comentarioEventoRepository.Listar(IdEvento));
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
                _comentarioEventoRepository.Deletar(id);
                return StatusCode(204);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPoridUsuario(Guid IdUsuario, Guid IdEvento)
        {
            try
            {
                return Ok(_comentarioEventoRepository.BuscarPoridUsuario(IdUsuario, IdEvento));
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
    }
}
