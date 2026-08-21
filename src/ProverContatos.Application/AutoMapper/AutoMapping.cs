using AutoMapper;
using ProverContatos.Communication.Responses;
using ProverContatos.Domain.Entities;

namespace ProverContatos.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Contato, ResponseContatoJson>();
        CreateMap<Contato, ResponseContatoResumidoJson>();
    }
}