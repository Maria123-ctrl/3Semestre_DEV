namespace UniversoHerois.Interfaces
{
    public interface IEquipeRepository
    {
        void Cadastrar(Equipe equipe);
        void Atualizar(Guid id, Equipe equipe);
        void Deletar(Guid id);
        List<Equipe> Listar();
    }
}
