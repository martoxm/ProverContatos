using AutoMapper;
using ProverContatos.Communication.Responses;
using ProverContatos.Domain.Repositories;
using ProverContatos.Exception.ExceptionsBase;

namespace ProverContatos.Application.UseCases.Contatos.Buscar;

public class BuscarContatoUseCase(IContatoRepository repository,
    IMapper mapper) : IBuscarContatoUseCase
{
    private readonly IContatoRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseContatoJson> ExecutarAsync(Guid id)
    {
        var contato = await _repository.BuscarAtivosPorIdAsync(id)
            ?? throw new NotFoundException("Contato não encontrado ou inativo.");

        return _mapper.Map<ResponseContatoJson>(contato);
    }
}