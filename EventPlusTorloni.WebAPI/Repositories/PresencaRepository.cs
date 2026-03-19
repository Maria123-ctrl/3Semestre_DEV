using EventPlusTorloni.WebAPI.BdContextEvent;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlusTorloni.WebAPI.Repositories
{
    public class PresencaRepository : IPresencaRepository
    {
        private readonly EventContext _eventContext;

        public PresencaRepository(EventContext eventContext)
        {
            _eventContext = eventContext;
        }

        public void Atualizar(Guid id, Presenca inscricao)
        {
            var PresencaBuscada = _eventContext.Presencas.Find(id);

            if (PresencaBuscada != null)
            {
                PresencaBuscada.Situacao = inscricao.Situacao;
                PresencaBuscada.IdEvento = inscricao.IdEvento;
                PresencaBuscada.IdPresenca = inscricao.IdPresenca;
                //O SaveChanges() detecta a mudança na propriedade "Título" automaticamente
                _eventContext.SaveChanges();
            }
        }

        /// <summary>
        /// Busca uma presença por id
        /// </summary>
        /// <param name="id">id da presença a ser buscada</param>
        /// <returns>presença buscada</returns>
        public Presenca BuscarPorId(Guid id)
        {
            return _eventContext.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).FirstOrDefault(p => p.IdPresenca == id)!;
        }

        public void Deletar(Guid id)
        {
            var PresencaBuscada = _eventContext.Presencas.Find(id);

            if (PresencaBuscada != null)
            {
                _eventContext.Presencas.Remove(PresencaBuscada);
                _eventContext.SaveChanges();
            }
        }

        public void Inscrever(Presenca Inscricao)
        {
            _eventContext.Presencas.Add(Inscricao);
            _eventContext.SaveChanges();
        }

        public List<Presenca> Listar()
        {
            return _eventContext.Presencas.OrderBy(Presenca => Presenca.Situacao).ToList();
        }

        /// <summary>
        /// Lista as presenças de um usuário para filtragem
        /// </summary>
        /// <param name="IdUsuario">id do usuário para filtragem</param>
        /// <returns>uma lista de presenças de um usuário específico</returns>
        public List<Presenca> ListarMinhas(Guid IdUsuario)
        {
            return _eventContext.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).Where(p => p.IdUsuario == IdUsuario).ToList();
        }
    }
}
