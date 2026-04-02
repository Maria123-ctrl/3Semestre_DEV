using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tarefa.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ICargoRepository _cargoRepository;
        public CargoController(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
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
                return Ok(_cargoRepository.Listar());
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
        public IActionResult Cadastrar([FromForm] CargoDTO cargo)
        {
            try
            {
                var novoCargo = new Cargo
                {
                    Titulo = cargo.Titulo!,
                    Descricao = cargo.Descricao!
                };
                _cargoRepository.Cadastrar(novoCargo);
                return Ok(novoCargo);
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
        public IActionResult Atualizar(Guid id, [FromForm] CargoDTO cargo)
        {
            try
            {
                var cargoAtualizado = new Cargo
                {
                    Titulo = cargo.Titulo!,
                    Descricao = cargo.Descricao!
                };
                _cargoRepository.Atualizar(id, cargoAtualizado);
                return Ok(cargoAtualizado);
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
                _cargoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        /// <summary>
        /// Endpoint da API que busca um cargo por seu Id no banco de dados
        /// </summary>
        /// <param name="id">Id do cargo a ser buscado</param>
        /// <returns>Status code 200 e os dados do cargo buscado</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id, [FromForm] Cargo cargo)
        {
            try
            {
                return Ok(_cargoRepository.BuscarPorId(id));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
