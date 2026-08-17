using ProverContatos.Communication.Requests;
using ProverContatos.Communication.Responses;

namespace ProverContatos.Application.UseCases.Contatos.Criar;

public interface ICriarContatoUseCase
{
    Task<ResponseContatoJson> ExecutarAsync(RequestCriarContatoJson request);
}