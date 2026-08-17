using ProverContatos.Communication.Requests;
using ProverContatos.Communication.Responses;
using ProverContatos.Domain.Entities;
using ProverContatos.Domain.Repositories;

namespace ProverContatos.Application.UseCases.Contatos.Criar;

public class CriarContatoUseCase(
    IContatoRepository repository,
    IUnityOfWork unityOfWork) : ICriarContatoUseCase
{
    private readonly IContatoRepository _repository = repository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;

    public async Task<ResponseContatoJson> ExecutarAsync(
        RequestCriarContatoJson request)
    {
        var contato = new Contato(
            request.Nome,
            request.DataNascimento,
            request.Sexo);

        await _repository.AdicionarAsync(contato);
        await _unityOfWork.CommitAsync();

        return new ResponseContatoJson
        {
            Id = contato.Id,
            Nome = contato.Nome,
            DataNascimento = contato.DataNascimento,
            Sexo = contato.Sexo,
            Idade = contato.Idade,
            Ativo = contato.Ativo
        };
    }
}