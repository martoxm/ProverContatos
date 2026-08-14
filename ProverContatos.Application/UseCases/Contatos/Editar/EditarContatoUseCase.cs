using ProverContatos.Communication.Requests;
using ProverContatos.Domain.Repositories;
using ProverContatos.Exception.ExceptionsBase;

namespace ProverContatos.Application.UseCases.Contatos.Editar;

public class EditarContatoUseCase(IContatoRepository repository, IUnityOfWork unityOfWork) : IEditarContatoUseCase
{
    private readonly IContatoRepository _repository = repository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;

    public async Task ExecutarAsync(long id, RequestEditarContatoJson request)
    {
        Validar(request);

        var contato = await _repository.BuscarAtivosPorIdAsync(id)
            ?? throw new NotFoundException("Contato não encontrado ou inativo.");

        contato.Nome = request.Nome;
        contato.DataNascimento = request.DataNascimento;
        contato.Sexo = request.Sexo;

        _repository.Atualizar(contato);
        await _unityOfWork.CommitAsync();
    }

    private static void Validar(RequestEditarContatoJson request)
    {
        var validator = new EditarContatoValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ProverException(errors);
        }
    }
}