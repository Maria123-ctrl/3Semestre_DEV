using EventPlusTorloni.WebAPI.Models;

namespace EventPlusTorloni.WebAPI.Interfaces
{
    public interface IPresencaRepository
    {
        void Inscrever(Presenca Inscricao);
        void Deletar(Guid id);
        List<Presenca> Listar();

        Presenca BuscarPorId(Guid id);
        void Atualizar(Guid id , Presenca inscricao);
        List<Presenca> ListarMinhas(Guid IdUsuario);
    }
} 
