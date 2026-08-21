using AutoMapper;
using ProverContatos.Communication.Responses;
using ProverContatos.Domain.Repositories;

namespace ProverContatos.Application.UseCases.Contatos.Listar;

public class ListarContatosUseCase(IContatoRepository repository,
    IMapper mapper) : IListarContatosUseCase
{
    private readonly IContatoRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<List<ResponseContatoResumidoJson>> ExecutarAsync()
    {
        var contatos = await _repository.ListarAtivosAsync();

        return _mapper.Map<List<ResponseContatoResumidoJson>>(contatos);
    }
}