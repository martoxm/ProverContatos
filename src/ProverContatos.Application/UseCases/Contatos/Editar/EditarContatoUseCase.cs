using ProverContatos.Communication.Requests;
using ProverContatos.Domain.Repositories;
using ProverContatos.Exception.ExceptionsBase;

namespace ProverContatos.Application.UseCases.Contatos.Editar;

public class EditarContatoUseCase(
    IContatoRepository repository,
    IUnityOfWork unityOfWork) : IEditarContatoUseCase
{
    private readonly IContatoRepository _repository = repository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;

    public async Task ExecutarAsync(
        Guid id,
        RequestEditarContatoJson request)
    {
        var contato = await _repository.BuscarAtivosPorIdAsync(id)
            ?? throw new NotFoundException("Contato não encontrado ou inativo.");

        contato.Atualizar(
            request.Nome,
            request.DataNascimento,
            request.Sexo);

        _repository.Atualizar(contato);
        await _unityOfWork.CommitAsync();
    }
}