using EventPlusTorloni.WebAPI.Models;

namespace EventPlusTorloni.WebAPI.Interfaces
{
    public interface IEventoRepository
    {
        void Cadstrar(Evento evento);
        List<Evento> Listar();

        void Deletar(Guid IdEvento);
        void Atualizar(Guid id, Evento evento);
        List<Evento> ListarPorId(Guid IdUsuario);
        List<Evento> ProximosEvetos();
        Evento BuscarPorId(Guid id);
    }
}
