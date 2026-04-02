namespace UniversoHerois.Interfaces
{
    public interface IMissaoRepository
    {
        void Cadastrar(Missao missao);
        void Atualizar(Guid id, Missao missao);
        void Deletar(Guid id);
        List<Missao> Listar();
        void SaveChanges();
    }
}
