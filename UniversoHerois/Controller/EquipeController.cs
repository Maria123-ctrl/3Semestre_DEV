using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversoHerois.DTO;
using UniversoHerois.Interfaces;

namespace UniversoHerois.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipeController : ControllerBase
    {
        private readonly IEquipeRepository _equipeRepository;
        public EquipeController(IEquipeRepository equipeRepository)
        {
            _equipeRepository = equipeRepository;
        }

        /// <summary>
        /// Endpoint para listar todos os cargos cadastrados no banco de dados.
        /// </summary>
        /// <returns>Status code 200 e uma lista com todos os cargos</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_equipeRepository.Listar());
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que cadastra um novo cargo no banco de dados
        /// </summary>
        /// <param name="cargo">Dados do cargo a ser cadastrado</param>
        /// <returns>Status code 200 e o cargo cadastrado</returns>
        [HttpPost]
        public IActionResult Cadastrar([FromForm] EquipeDTO equipe)
        {
            try
            {
                var novaEquipe = new Equipe
                {
                    Nome = equipe.Nome!,
                };
                _equipeRepository.Cadastrar(novaEquipe);
                return Ok(novaEquipe);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que atualiza um cargo já cadastrado no banco de dados
        /// </summary>
        /// <param name="id">Id do cargo a ser atualizado</param>
        /// <param name="equipe">Dados do cargo atualizado</param>
        /// <returns>Status code 200 e o cargo atualizado</returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, [FromForm] EquipeDTO cargo)
        {
            try
            {
                var equipeAtualizado = new Equipe
                {
                    Nome = equipe.Nome!
                };
                _equipeRepository.Atualizar(id, equipeAtualizado);
                return Ok(equipeAtualizado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que deleta um cargo cadastrado no banco de dados
        /// </summary>
        /// <param name="id">Id do cargo a ser deletado</param>
        /// <returns>Status code 204</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                _equipeRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
