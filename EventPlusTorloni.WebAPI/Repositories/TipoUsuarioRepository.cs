using EventPlusTorloni.WebAPI.BdContextEvent;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;

namespace EventPlusTorloni.WebAPI.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {

        private readonly EventContext _context;

        // Injeção de dependência: Recebe o contexto pelo construtor
        public TipoUsuarioRepository(EventContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Atualiza um tipo de evento usando o rastreamento automático
        /// </summary>
        /// <param name="id">id do tipo Usuario a ser atualizado</param>
        /// <param name="tipoEvento">Novos dados do tipo Usuario</param>
        public void Atualizar(Guid id, TipoUsuario tipoUsuario)
        {
            var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);

            if (tipoUsuarioBuscado != null)
            {
                tipoUsuarioBuscado.Titulo = tipoUsuario.Titulo;

                //O SaveChanges() detecta a mudança na propriedade "Título" automaticamente
                _context.SaveChanges();
            }
        }

        public TipoUsuario BuscarPorId(Guid id)
        {
            return _context.TipoUsuarios.Find(id)!;
        }

        public void Cadastrar(TipoUsuario tipoUsuario)
        {
            _context.TipoUsuarios.Add(tipoUsuario);
            _context.SaveChanges();
        }

        public void Deletar(Guid id)
        {
            var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);

            if (tipoUsuarioBuscado != null)
            {
                _context.TipoUsuarios.Remove(tipoUsuarioBuscado);
                _context.SaveChanges();
            }
        }

        public List<TipoUsuario> Listar()
        {
            return _context.TipoUsuarios.OrderBy(tipoUsuario => tipoUsuario.Titulo).ToList();
        }
    }
}
