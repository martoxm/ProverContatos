namespace ProverContatos.Application.UseCases.Contatos.Desativar;

public interface IDesativarContatoUseCase
{
    Task ExecutarAsync(Guid id);
}