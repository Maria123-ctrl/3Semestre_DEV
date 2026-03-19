using EventPlusTorloni.WebAPI.BdContextEvent;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlusTorloni.WebAPI.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly EventContext _context;

        public EventoRepository(EventContext context) 
        { 
            _context = context; 
        }


        /// <summary>
        /// Método que atualiza os dados de um evento já existente
        /// </summary>
        /// <param name="id">Id do evento a ser atualizado</param>
        /// <param name="evento">Evento com os dados atualizados</param>
        public void Atualizar(Guid id, Evento evento)
        {
            var EventoBuscado = _context.Eventos.Find(id);

            if (EventoBuscado != null)
            {
                EventoBuscado.Nome = String.IsNullOrWhiteSpace(evento.Nome) ? EventoBuscado.Nome : evento.Nome;
                EventoBuscado.Descricao = String.IsNullOrWhiteSpace(evento.Descricao) ? EventoBuscado.Descricao : evento.Descricao;
                EventoBuscado.DataEvento = evento.DataEvento;
                EventoBuscado.IdTipoEvento = evento.IdTipoEvento;
                EventoBuscado.IdInstituicao = evento.IdInstituicao;

                //O SaveChanges() detecta a mudança na propriedade "Título" automaticamente
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Método que busca um evento especifico pelo seu id 
        /// </summary>
        /// <param name="id">Id do evento buscado</param>
        /// <returns>Evento buscado</returns>
        public Evento BuscarPorId(Guid id)
        {
            return _context.Eventos.Find(id)!;
        }

        /// <summary>
        /// Método que cadastra um novo evento
        /// </summary>
        /// <param name="evento">Dados do evento cadastrado</param>
        public void Cadastrar(Evento evento)
        {
            _context.Eventos.Add(evento);
            _context.SaveChanges();
        }

        /// <summary>
        /// Método que deleta um evento pelo seu id
        /// </summary>
        /// <param name="id">Id do evento a ser deletado</param>
        public void Deletar(Guid id)
        {
            var EventoBuscado = _context.Eventos.Find(id);

            if (EventoBuscado != null)
            {
                _context.Eventos.Remove(EventoBuscado);
                _context.SaveChanges();
            }
        }

        public List<Evento> Listar()
        {
            return _context.Eventos.OrderBy(Evento => Evento.Nome).ToList();
        }

        /// <summary>
        /// Método que busca eventos no qual um usuário confitmou presença
        /// </summary>
        /// <param name="IdUsuario">Id do usuário a ser buscado</param>
        /// <returns>Uma lista de eventos</returns>
        public List<Evento> ListarPorId(Guid IdUsuario)
        {
            return _context.Eventos.Include(e => e.IdTipoEventoNavigation).Include(e => e.IdInstituicaoNavigation).Where(e => e.Presencas.Any(p => p.IdUsuario == IdUsuario && p.Situacao == true)).ToList();
        }


        /// <summary>
        /// Método que traz a lista dos próximos eventos
        /// </summary>
        /// <returns>Uma lista de eventos</returns>
        public List<Evento> ProximosEvetos()
        {
            return _context.Eventos.Include(e => e.IdTipoEventoNavigation).Include(e => e.IdInstituicaoNavigation).Where(e => e.DataEvento >= DateTime.Now).OrderBy(e => e.DataEvento).ToList();
        }
    }
}
