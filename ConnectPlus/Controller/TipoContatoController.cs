using ConnectPlus.DTO;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoContatoController : ControllerBase
    {
        private ITipoContatoRepository _tipoContatoRepository;

        public TipoContatoController(ITipoContatoRepository tipoContatoRepository)
        {
            _tipoContatoRepository = tipoContatoRepository;
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
                return Ok(_tipoContatoRepository.Listar());

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
                return Ok(_tipoContatoRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }


        /// <summary>
        /// Endpoint da API que faz a chamada para o método de cadastrar um tipo de evento
        /// </summary>
        /// <param name="tipoEvento">Tipo de evento a ser cadastrado</param>
        /// <returns>Status code 201 e o tipo de evento cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar(TipoContatoDTO tipoContato)
        {
            try
            {
                var novoTipoContato = new TipoContato
                {
                    Titulo = tipoContato.Titulo!
                };

                _tipoContatoRepository.Cadastrar(novoTipoContato);

                return StatusCode(201, novoTipoContato);
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
        /// <param name="tipoContato">Tipo de evento com os dados atualizados</param>
        /// <returns>Status code 204 e o tipo de evento atualizado</returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, TipoContatoDTO tipoContato)
        {
            try
            {
                var tipoContatoAtualizado = new TipoContato
                {
                    Titulo = tipoContato.Titulo!
                };

                _tipoContatoRepository.Atualizar(id, tipoContatoAtualizado);

                return StatusCode(204, tipoContatoAtualizado);
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
                _tipoContatoRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
    }
}

