using ConnectPlus.BdContextConect;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using static ConnectPlus.Repositories.TipoContatoRepository;

namespace ConnectPlus.Repositories
{
    public class TipoContatoRepository : ITipoContatoRepository
    {
            private readonly CContext _context;

            public TipoContatoRepository(CContext context)
            {
                _context = context;
            }

            // Injeção de dependência: Recebe o contexto pelo construtor

            /// <summary>
            /// Atualiza um tipo de evento usando o rastreamento automático
            /// </summary>
            /// <param name="id">id do tipo evento a ser atualizado</param>
            /// <param name="tipoEvento">Novos dados do tipo evento</param>

            public void Atualizar(Guid id, TipoContato tipoContato)
            {
                var tipoContatoBuscado = _context.TipoContatos.Find(id);

                if (tipoContatoBuscado != null)
                {
                tipoContatoBuscado.Titulo = tipoContato.Titulo;

                    //O SaveChanges() detecta a mudança na propriedade "Título" automaticamente
                    _context.SaveChanges();
                }
            }

        /// <summary>
        /// Buscar um tipo de evento por id
        /// </summary>
        /// <param name="id">id do tipo evento a ser buscado</param>
        /// <returns>Objeto do tipoEvento com as informações do tipo de evento buscado</returns>
            [HttpPost]
            public void Cadastrar(TipoContato tipoContato)
            {
                _context.TipoContatos.Add(tipoContato);
                _context.SaveChanges();
            }



        /// <summary>
        /// Deleta um tipo de evento
        /// </summary>
        /// <param name="id">id do tipo evento a ser deletado</param>
        public void Deletar(Guid id)
            {
                var tipoContatoBuscado = _context.TipoContatos.Find(id);

                if (tipoContatoBuscado != null)
                {
                    _context.TipoContatos.Remove(tipoContatoBuscado);
                    _context.SaveChanges();
                }
            }


            /// <summary>
            /// Busca a lista de tipo de eventos cadastrado
            /// </summary>
            /// <returns>Uma lista de tipo eventos</returns>
            public List<TipoContato> Listar()
            {
                return _context.TipoContatos.OrderBy(tipoContato => tipoContato.Titulo).ToList();
            }

        TipoContato ITipoContatoRepository.BuscarPorId(Guid id)
        {
            return _context.TipoContatos.Find(id)!;
        }

        List<TipoContato> ITipoContatoRepository.Listar()
        {
            return _context.TipoContatos.OrderBy(tipoContato => tipoContato.Titulo).ToList();
        }
    }
    }

