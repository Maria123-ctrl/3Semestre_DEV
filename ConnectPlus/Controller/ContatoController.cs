using ConnectPlus.DTO;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;
using ConnectPlus.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConnectPlus.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoController(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromForm] ContatoDTO contato)
        {
            var contatoBuscado = _contatoRepository.BuscarPorId(id);
            if (contatoBuscado == null)
            {
                return NotFound("Contato não encontrado");
            }
            if (contato.Imagem != null && contato.Imagem.Length > 0)
            {
                var pastaRelativa = "wwwroot/imagens";
                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);
                if (!String.IsNullOrEmpty(contatoBuscado.Imagem))
                {
                    var caminhoAntigo = Path.Combine(caminhoPasta, contatoBuscado.Imagem);
                    if (System.IO.File.Exists(caminhoAntigo))
                    {
                        System.IO.File.Delete(caminhoAntigo);
                    }
                }

                var extensao = Path.GetExtension(contato.Imagem.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                if (!Directory.Exists(caminhoPasta))
                {
                    Directory.CreateDirectory(caminhoPasta);
                }
                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    await contato.Imagem.CopyToAsync(stream);
                }
                contatoBuscado.Imagem = nomeArquivo;
            }
            try
            {
                _contatoRepository.Atualizar(id, contatoBuscado);
                return Ok(contatoBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
            /// <summary>
            /// Endpoint da API que faz a chamada para o método de Buscar um usuário por id
            /// </summary>
            /// <returns>id do usuário a ser buscado</returns>
            [HttpGet("{id}")]
            public IActionResult BuscarPorId(Guid id)
            {
                try
                {
                    return Ok(_contatoRepository.BuscarPorId(id));
                }
                catch (Exception erro)
                {
                    return BadRequest(erro.Message);
                }

            }



            /// <summary>
            /// Endpoint da API que faz a chamada para o método cadastrar
            /// </summary>
            /// <param name="usuario">Usuário a ser cadastrado</param>
            /// <returns>StatusCode 201 e o usuário cadastrado</returns>
            [HttpPost]
            public IActionResult Cadastrar(ContatoDTO contato)
            {
            if (String.IsNullOrEmpty(contato.Nome) || String.IsNullOrEmpty(contato.FormaContato))
            {
                return BadRequest("O nome e a forma de contato são obrigatórios");
            }
            Contato novoContato = new Contato();
            if (contato.Imagem != null && contato.Imagem.Length > 0)
            {
                var extensao = Path.GetExtension(contato.Imagem.FileName);
                var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

                var pastaRelativa = "wwwroot/imagens";
                var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

                if (!Directory.Exists(caminhoPasta))
                {
                    Directory.CreateDirectory(caminhoPasta);
                }

                var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);
                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    contato.Imagem.CopyToAsync(stream);
                }

                novoContato.Imagem = nomeArquivo;
            }
            novoContato.Nome = contato.Nome;
            novoContato.FormaContato = contato.FormaContato;
            novoContato.IdTipoContato = contato.IdTipoContato;
            try
            {
                _contatoRepository.Cadastrar(novoContato);
                return Ok(novoContato);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        }
    }


