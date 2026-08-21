using AutoMapper;
using ProverContatos.Communication.Requests;
using ProverContatos.Communication.Responses;
using ProverContatos.Domain.Entities;
using ProverContatos.Domain.Repositories;

namespace ProverContatos.Application.UseCases.Contatos.Criar;

public class CriarContatoUseCase(
    IContatoRepository repository,
    IUnityOfWork unityOfWork,
    IMapper mapper) : ICriarContatoUseCase
{
    private readonly IContatoRepository _repository = repository;
    private readonly IUnityOfWork _unityOfWork = unityOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseContatoJson> ExecutarAsync(
        RequestCriarContatoJson request)
    {
        var contato = Contato.Criar(
            request.Nome,
            request.DataNascimento,
            request.Sexo);
        ;

        await _repository.AdicionarAsync(contato);
        await _unityOfWork.CommitAsync();

        return _mapper.Map<ResponseContatoJson>(contato);
    }
}