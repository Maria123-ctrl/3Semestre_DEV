namespace UniversoHerois.Interfaces
{
    public interface IHeroiRepository
    {
        void Cadastrar(Heroi heroi);
        void Atualizar(Guid id, Heroi heroi);
        void Deletar(Guid id);
        List<Heroi> Listar();
    }
}
