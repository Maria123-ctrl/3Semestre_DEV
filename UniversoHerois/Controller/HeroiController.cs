using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversoHerois.DTO;
using UniversoHerois.Interfaces;
using UniversoHerois.Repositories;

namespace UniversoHerois.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroiController : ControllerBase
    {
            private readonly IEquipeRepository _equipeRepository;
            public HeroiController(IHeroiRepository heroiRepository)
            {
                _heroiRepository = heroiRepository;
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
                    return Ok(_heroiRepository.Listar());
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
            public IActionResult Cadastrar([FromForm] HeroiDTO heroi)
            {
                try
                {
                    var novoHeroi = new Heroi
                    {
                        Personagem = heroi.Personagem!,
                        Poder = heroi.Poder!,
                    };
                _heroiRepository.Cadastrar(novoHeroi);
                    return Ok(novoHeroi);
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
            /// <param name="cargo">Dados do cargo atualizado</param>
            /// <returns>Status code 200 e o cargo atualizado</returns>
            [HttpPut("{id}")]
            public IActionResult Atualizar(Guid id, [FromForm] HeroiDTO heroi)
            {
                try
                {
                    var heroiAtualizado = new Heroi
                    {
                        Personagem = heroi.Personagem!,
                        Poder = heroi.Poder!
                    };
                    _heroiRepository.Atualizar(id, heroiAtualizado);
                    return Ok(heroiAtualizado);
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
                _heroiRepository.Deletar(id);
                    return NoContent();
                }
                catch (Exception erro)
                {
                    return BadRequest(erro.Message);
                }
            }
        }
}
