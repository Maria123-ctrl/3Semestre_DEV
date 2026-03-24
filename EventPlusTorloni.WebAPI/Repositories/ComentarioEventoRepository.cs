using EventPlusTorloni.WebAPI.BdContextEvent;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlusTorloni.WebAPI.Repositories
{
    public class ComentarioEventoRepository : IComentarioEventoRepository
    {
        private readonly EventContext _context;

        // Injeção de dependência: Recebe o contexto pelo construtor
        public ComentarioEventoRepository(EventContext context)
        {
            _context = context;
        }
        public void Atualizar(Guid id, ComentarioEvento comentarioEvento)
        {
            var ComentarioEventoBuscado = _context.ComentarioEventos.Find(id);

            if (ComentarioEventoBuscado != null)
            {
                ComentarioEventoBuscado.Descricao = comentarioEvento.Descricao;
                ComentarioEventoBuscado.Exibe = comentarioEvento.Exibe;
                ComentarioEventoBuscado.DataComentarioEvento = comentarioEvento.DataComentarioEvento;

                //O SaveChanges() detecta a mudança na propriedade "Título" automaticamente
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Método que busca um evento especifico pelo seu id 
        /// </summary>
        /// <param name="id">Id do evento buscado</param>
        /// <returns>Evento buscado</returns>

        public ComentarioEvento BuscarPoridUsuario(Guid IdUsuario, Guid IdEvento)
        {
            var comentarioBuscado = _context.ComentarioEventos.FirstOrDefault(c => c.IdUsuario == IdUsuario && c.IdEvento == IdEvento);
            if (comentarioBuscado != null)
            {
                return comentarioBuscado;
            }
            else
            {
                return null!;
            }

        }

        /// <summary>
        /// Método que cadastra um novo evento
        /// </summary>
        /// <param name="evento">Dados do evento cadastrado</param>
        public void Cadastrar(ComentarioEvento comentarioEvento)
        {
            _context.ComentarioEventos.Add(comentarioEvento);
            _context.SaveChanges();
        }

        /// <summary>
        /// Método que deleta um evento pelo seu id
        /// </summary>
        /// <param name="id">Id do evento a ser deletado</param>
        public void Deletar(Guid id)
        {
            var ComentarioEventoBuscado = _context.ComentarioEventos.Find(id);

            if (ComentarioEventoBuscado != null)
            {
                _context.ComentarioEventos.Remove(ComentarioEventoBuscado);
                _context.SaveChanges();
            }
        }

        public List<ComentarioEvento> Listar(Guid IdEvento)
        {
            return _context.ComentarioEventos.Where(c => c.IdEvento == IdEvento).ToList();
        }

        public List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento)
        {
            return _context.ComentarioEventos
            .Include(c => c.IdUsuarioNavigation)
            .Include(c => c.IdEventoNavigation)
            .Where(c => c.Exibe == true && c.IdEvento == IdEvento)
            .ToList();
        }
    }
}
