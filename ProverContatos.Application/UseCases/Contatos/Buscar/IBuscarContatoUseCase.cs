using ProverContatos.Communication.Responses;

namespace ProverContatos.Application.UseCases.Contatos.Buscar;

public interface IBuscarContatoUseCase
{
    Task<ResponseContatoJson> ExecutarAsync(long id);
}