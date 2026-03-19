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
    public class InstituicaoController : ControllerBase
    {
        private IInstituicaoRepository _intituicaoRepository;

        public InstituicaoController(IInstituicaoRepository instituicaoRepository)
        {
            _intituicaoRepository = instituicaoRepository;
        }



        /// <summary>
        /// Endpoint da API que faz chamada para o método de listar os tipos de instituicao
        /// </summary>
        /// <returns>Status code 200 e a lista de tipos de instituicao</returns>

        [Authorize]
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_intituicaoRepository.Listar());

            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que faz a chamada para um método de busca de um tipo de instuicao específico
        /// </summary>
        /// <param name="id">Id do tipo de evento buscado</param>
        /// <returns>Status code 200 e tipo de evento buscado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_intituicaoRepository.BuscarPorId(id));
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
        public IActionResult Cadastrar(InstituicaoDTO instituicao)
        {
            try
            {
                var novaInstituicao = new Instituicao
                {
                    NomeFantasia = instituicao.NomeFantasia!,
                    Cnpj = instituicao.Cnpj!,
                    Endereco = instituicao.Endereco!

                };

                _intituicaoRepository.Cadastrar(novaInstituicao);

                return StatusCode(201, novaInstituicao);
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
        public IActionResult Atualizar(Guid id, InstituicaoDTO instituicao)
        {
            try
            {
                var instituicaoAtualizada = new Instituicao
                {
                    NomeFantasia = instituicao.NomeFantasia!,
                    Cnpj = instituicao.Cnpj!,
                    Endereco = instituicao.Endereco!
                };

                _intituicaoRepository.Atualizar(id, instituicaoAtualizada);

                return StatusCode(204, instituicaoAtualizada);
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
                _intituicaoRepository.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }
    }
}
