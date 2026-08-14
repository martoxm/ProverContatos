using ProverContatos.Domain.Entities;

namespace ProverContatos.Domain.Repositories;

public interface IContatoRepository
{
    Task AdicionarAsync(Contato contato);
    Task<List<Contato>> ListarAtivosAsync();
    Task<Contato?> BuscarPorIdAsync(long id);
    Task<Contato?> BuscarAtivosPorIdAsync(long id);
    void Atualizar(Contato contato);
    void Excluir(Contato contato);
}