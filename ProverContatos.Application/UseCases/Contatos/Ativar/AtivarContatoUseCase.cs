using ProverContatos.Domain.Repositories;
using ProverContatos.Exception.ExceptionsBase;

namespace ProverContatos.Application.UseCases.Contatos.Ativar;

public class AtivarContatoUseCase(
    IContatoRepository repository,
    IUnityOfWork unityOfWork) : IAtivarContatoUseCase
{
    private readonly IContatoRepository _repository = repository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;

    public async Task ExecutarAsync(long id)
    {
        var contato = await _repository.BuscarPorIdAsync(id)
            ?? throw new NotFoundException(
                "Contato não encontrado.");

        if (contato.Ativo)
        {
            throw new ProverException(
                "Contato já está ativo.");
        }

        contato.Ativar();

        _repository.Atualizar(contato);

        await _unityOfWork.CommitAsync();
    }
}