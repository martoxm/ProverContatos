using ProverContatos.Communication.Responses;

namespace ProverContatos.Application.UseCases.Contatos.Listar;

public interface IListarContatosUseCase
{
    Task<List<ResponseContatoResumidoJson>> ExecutarAsync();
}