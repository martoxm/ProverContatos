using ProverContatos.Domain.Repositories;
using ProverContatos.Infrastructure.Data;

namespace ProverContatos.Infrastructure.Repositories;

public class UnityOfWork(ProverContatosDbContext context) : IUnityOfWork
{
    private readonly ProverContatosDbContext _context = context;

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}