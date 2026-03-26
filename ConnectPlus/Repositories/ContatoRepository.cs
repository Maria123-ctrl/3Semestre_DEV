using ConnectPlus.BdContextConect;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;

namespace ConnectPlus.Repositories 
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly CContext _context;

        public ContatoRepository(CContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Método que atualiza os dados de um evento já existente
        /// </summary>
        /// <param name="id">Id do evento a ser atualizado</param>
        /// <param name="contato">Evento com os dados atualizados</param>
        public void Atualizar(Guid id, Contato contato)
        {
            var ContatoBuscado = _context.Contatos.Find(id);

            if (ContatoBuscado != null)
            {
                ContatoBuscado.Nome = String.IsNullOrWhiteSpace(contato.Nome) ? ContatoBuscado.Nome : contato.Nome;
                ContatoBuscado.FormaContato = String.IsNullOrWhiteSpace(contato.FormaContato) ? ContatoBuscado.FormaContato : contato.FormaContato;
                ContatoBuscado.Imagem = contato.Imagem;
                ContatoBuscado.IdTipoContato = contato.IdTipoContato;

                //O SaveChanges() detecta a mudança na propriedade "Título" automaticamente
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Método que busca um evento especifico pelo seu id 
        /// </summary>
        /// <param name="id">Id do evento buscado</param>
        /// <returns>Evento buscado</returns>
        public Contato BuscarPorId(Guid id)
        {
            return _context.Contatos.Find(id)!;
        }

        /// <summary>
        /// Método que cadastra um novo evento
        /// </summary>
        /// <param name="contato">Dados do evento cadastrado</param>
        public void Cadastrar(Contato contato)
        {
            _context.Contatos.Add(contato);
            _context.SaveChanges();
        }

        /// <summary>
        /// Método que deleta um evento pelo seu id
        /// </summary>
        /// <param name="id">Id do evento a ser deletado</param>
        public void Deletar(Guid id)
        {
            var ContatoBuscado = _context.Contatos.Find(id);

            if (ContatoBuscado != null)
            {
                _context.Contatos.Remove(ContatoBuscado);
                _context.SaveChanges();
            }
        }

        public List<Contato> Listar()
        {
            return _context.Contatos.OrderBy(Evento => Evento.Nome).ToList();
        }
    }
}

