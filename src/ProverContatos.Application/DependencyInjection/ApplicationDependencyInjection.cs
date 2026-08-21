using Microsoft.Extensions.DependencyInjection;
using ProverContatos.Application.AutoMapper;
using ProverContatos.Application.UseCases.Contatos.Ativar;
using ProverContatos.Application.UseCases.Contatos.Buscar;
using ProverContatos.Application.UseCases.Contatos.Criar;
using ProverContatos.Application.UseCases.Contatos.Desativar;
using ProverContatos.Application.UseCases.Contatos.Editar;
using ProverContatos.Application.UseCases.Contatos.Excluir;
using ProverContatos.Application.UseCases.Contatos.Listar;

namespace ProverContatos.Application.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddCasosDeUso(services);
        AddAutoMapper(services);
    }

    private static void AddCasosDeUso(this IServiceCollection services)
    {
        services.AddScoped<ICriarContatoUseCase, CriarContatoUseCase>();
        services.AddScoped<IListarContatosUseCase, ListarContatosUseCase>();
        services.AddScoped<IBuscarContatoUseCase, BuscarContatoUseCase>();
        services.AddScoped<IEditarContatoUseCase, EditarContatoUseCase>();
        services.AddScoped<IDesativarContatoUseCase, DesativarContatoUseCase>();
        services.AddScoped<IAtivarContatoUseCase, AtivarContatoUseCase>();
        services.AddScoped<IExcluirContatoUseCase, ExcluirContatoUseCase>();
    }

    private static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(AutoMapping));
    }
}