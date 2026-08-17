using ProverContatos.Communication.Requests;

namespace ProverContatos.Application.UseCases.Contatos.Editar;

public interface IEditarContatoUseCase
{
    Task ExecutarAsync(Guid id, RequestEditarContatoJson request);
}