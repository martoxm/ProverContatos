namespace ProverContatos.Domain.Repositories;

public interface IUnityOfWork
{
    Task CommitAsync();
}