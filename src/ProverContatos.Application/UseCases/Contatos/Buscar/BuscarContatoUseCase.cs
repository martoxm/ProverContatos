using ProverContatos.Communication.Responses;
using ProverContatos.Domain.Repositories;
using ProverContatos.Exception.ExceptionsBase;

namespace ProverContatos.Application.UseCases.Contatos.Buscar;

public class BuscarContatoUseCase(IContatoRepository repository) : IBuscarContatoUseCase
{
    private readonly IContatoRepository _repository = repository;

    public async Task<ResponseContatoJson> ExecutarAsync(Guid id)
    {
        var contato = await _repository.BuscarAtivosPorIdAsync(id)
            ?? throw new NotFoundException("Contato não encontrado ou inativo.");

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