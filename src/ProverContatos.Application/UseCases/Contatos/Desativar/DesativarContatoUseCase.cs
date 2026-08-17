using ProverContatos.Domain.Repositories;
using ProverContatos.Exception.ExceptionsBase;

namespace ProverContatos.Application.UseCases.Contatos.Desativar;

public class DesativarContatoUseCase(
    IContatoRepository repository,
    IUnityOfWork unityOfWork) : IDesativarContatoUseCase
{
    private readonly IContatoRepository _repository = repository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;

    public async Task ExecutarAsync(Guid id)
    {
        var contato = await _repository.BuscarAtivosPorIdAsync(id)
            ?? throw new NotFoundException(
                "Contato não encontrado ou já inativo.");

        contato.Desativar();

        _repository.Atualizar(contato);

        await _unityOfWork.CommitAsync();
    }
}