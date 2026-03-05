using FilmesTorloni.WebAPI.Controllers;
using FilmesTorloni.WebAPI.Models;

namespace FilmesTorloni.WebAPI.Interfaces
{
    public interface IFilmeRepository
    {
        void Cadastrar(Filme novoilme);
        void AtualizarIdCorpo(Filme filmeAtualizado);
        void AtualizarIdUrl(Guid id, Filme filmeAtualizado);
        List<Filme> Listar();
        void Deletar(Guid id);
        Filme BuscarPorId(Guid id);
        void Cadastrar(FilmeController novoFilme);
    }
}
