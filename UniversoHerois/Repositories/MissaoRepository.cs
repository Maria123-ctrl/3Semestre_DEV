using UniversoHerois.Interfaces;

namespace UniversoHerois.Repositories
{
    public class MissaoRepository: IMissaoRepository
    {
        private readonly IMissaoRepository _missaoRepository;
        public MissaoRepository(IMissaoRepository _missaoRepository)
        {
            MissaoRepository = _missaoRepository;
        }
        /// <summary>
        /// Método que atualiza um cargo existente.
        /// </summary>
        /// <param name="id">Id do cargo a ser atualizado</param> 
        /// <param name="missao">Dados do cargo atualizado</param>
        public void Atualizar(Guid id, Missao missao)
        {
            var missaoBuscado = _missaoRepository.Missao.Find(id);
            if (missaoBuscado != null)
            {
                missaoBuscado.Titulo = String.IsNullOrEmpty(missao.Titulo) ? missaoBuscado.Titulo : missao.Titulo;
                missaoBuscado.Descricao = String.IsNullOrEmpty(missao.Descricao) ? missaoBuscado.Descricao : missao.Descricao;
                _missaoRepository.Missao.Update(missaoBuscado);
                _missaoRepository.SaveChanges();
            }
        }

        public void Atualizar(Guid id, Missao missao)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que busca um cargo por seu Id.
        /// </summary>
        /// <param name="id">Id do cargo a ser buscado</param> 
        /// <returns>Daados do cargo buscado</returns>
        public Cargo BuscarPorId(Guid id)
        {
            return _context.Cargos.Find(id)!;
        }
        /// <summary>
        /// Método que cadastra um novo cargo no banco de dados.
        /// </summary>
        /// <param name="cargo">Dados do cargo a ser cadastrado</param>
        public void Cadastrar(Cargo cargo)
        {
            _context.Cargos.Add(cargo);
            _context.SaveChanges();
        }

        public void Cadastrar(Missao missao)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que deleta um cargo do banco de dados.
        /// </summary>
        /// <param name="id">Id do cargo a ser deletado</param>
        public void Deletar(Guid id)
        {
            var cargoBuscado = _context.Cargos.Find(id);
            if (cargoBuscado != null)
            {
                _context.Cargos.Remove(cargoBuscado);
                _context.SaveChanges();
            }
        }
        /// <summary>
        /// Método que lista todos os cargos cadastrados no banco de dados.
        /// </summary>
        /// <returns>Uma lista com todos os cargos cadastrados no banco de dados</returns>
        public List<Cargo> Listar()
        {
            return _context.Cargos.ToList();
        }

        List<Missao> IMissaoRepository.Listar()
        {
            throw new NotImplementedException();
        }
    }
}
