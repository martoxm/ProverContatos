using ProverContatos.Communication.Responses;
using ProverContatos.Domain.Repositories;

namespace ProverContatos.Application.UseCases.Contatos.Listar;

public class ListarContatosUseCase(IContatoRepository repository) : IListarContatosUseCase
{
    private readonly IContatoRepository _repository = repository;

    public async Task<List<ResponseContatoResumidoJson>> ExecutarAsync()
    {
        var contatos = await _repository.ListarAtivosAsync();

        return contatos.Select(c => new ResponseContatoResumidoJson
        {
            Id = c.Id,
            Nome = c.Nome,
            Idade = c.Idade,
            Sexo = c.Sexo
        }).ToList();
    }
}