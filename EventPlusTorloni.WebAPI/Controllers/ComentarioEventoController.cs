using Azure;
using Azure.AI.ContentSafety;
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
        private readonly ContentSafetyClient _contentSafetyClient;
        private readonly IComentarioEventoRepository _comentarioEventoRepository;

        public ComentarioEventoController( ContentSafetyClient contentSafetyClient, IComentarioEventoRepository comentarioEventoRepository)
        {
            _contentSafetyClient = contentSafetyClient;
            _comentarioEventoRepository = comentarioEventoRepository;
        }

        /// <summary>
        /// Endpoint da API que cadastra e modera um comentário
        /// </summary>
        /// <param name="evento">comentário a ser moderado</param>
        /// <returns>Status code 201 e o comentário criado</returns>
        [HttpPost]
        public async Task<IActionResult> Cadastrar(ComentarioEventoDTO comentarioEvento)
        {
            try
            {
                if (string.IsNullOrEmpty(comentarioEvento.Descricao))
                {
                    return BadRequest("O texto a ser moderado não pode estar vazio.");
                }

                // criar objeto de análise
                var request =new AnalyzeTextOptions(comentarioEvento.Descricao);

                // chamar a API do Azure Content Safety
                Response<AnalyzeTextResult> response = await
                    _contentSafetyClient.AnalyzeTextAsync(request);

                // Verificar se o texto tem alguma severidade maior que 0
                bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(comentarioEvento => comentarioEvento.Severity > 0);

                var novoComentarioEvento = new ComentarioEvento

                {

                    Descricao = comentarioEvento.Descricao!,
                    Exibe = !temConteudoImproprio,
                    DataComentarioEvento = comentarioEvento.DataComentrioEvento,
                    IdEvento = comentarioEvento.IdEvento,
                    IdUsuario = comentarioEvento.IdUsuario!
                };
                _comentarioEventoRepository.Cadastrar(novoComentarioEvento);
                return StatusCode(201 , novoComentarioEvento);
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

        [HttpGet("{idUsuario}/{idEvento}")]
        public IActionResult BuscarPoridUsuario(Guid IdUsuario, Guid IdEvento)
        {
            try
            {
                var comentario = _comentarioEventoRepository.BuscarPoridUsuario(IdUsuario, IdEvento);

                if(comentario == null)
                {
                    return NotFound("Comentário não encontrado.");
                }
                return Ok(_comentarioEventoRepository.BuscarPoridUsuario(IdUsuario, IdEvento));
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
    }
}
