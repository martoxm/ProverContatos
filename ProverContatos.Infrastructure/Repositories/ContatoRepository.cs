using Microsoft.EntityFrameworkCore;
using ProverContatos.Domain.Entities;
using ProverContatos.Domain.Repositories;
using ProverContatos.Infrastructure.Data;

namespace ProverContatos.Infrastructure.Repositories;

public class ContatoRepository(ProverContatosDbContext context) : IContatoRepository
{
    private readonly ProverContatosDbContext _context = context;

    public async Task AdicionarAsync(Contato contato)
    {
        await _context.Contatos.AddAsync(contato);
    }

    public async Task<List<Contato>> ListarAtivosAsync()
    {
        return await _context.Contatos
            .Where(c => c.Ativo)
            .ToListAsync();
    }

    public async Task<Contato?> BuscarPorIdAsync(long id)
    {
        return await _context.Contatos
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Contato?> BuscarAtivosPorIdAsync(long id)
    {
        return await _context.Contatos
            .FirstOrDefaultAsync(c => c.Id == id && c.Ativo);
    }

    public void Atualizar(Contato contato)
    {
        _context.Contatos.Update(contato);
    }

    public void Excluir(Contato contato)
    {
        _context.Contatos.Remove(contato);
    }
}