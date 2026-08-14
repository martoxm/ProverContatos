using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProverContatos.Domain.Repositories;
using ProverContatos.Infrastructure.Data;
using ProverContatos.Infrastructure.Repositories;

namespace ProverContatos.Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        services.AddDbContext<ProverContatosDbContext>(options =>
            options.UseSqlite(connectionString));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IContatoRepository, ContatoRepository>();
        services.AddScoped<IUnityOfWork, UnityOfWork>();
    }
}