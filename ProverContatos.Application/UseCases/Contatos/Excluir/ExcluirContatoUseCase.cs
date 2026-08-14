using ProverContatos.Domain.Repositories;
using ProverContatos.Exception.ExceptionsBase;

namespace ProverContatos.Application.UseCases.Contatos.Excluir;

public class ExcluirContatoUseCase(IContatoRepository repository, IUnityOfWork unityOfWork) : IExcluirContatoUseCase
{
    private readonly IContatoRepository _repository = repository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;

    public async Task ExecutarAsync(long id)
    {
        var contato = await _repository.BuscarPorIdAsync(id)
            ?? throw new NotFoundException("Contato não encontrado.");

        _repository.Excluir(contato);
        await _unityOfWork.CommitAsync();
    }
}