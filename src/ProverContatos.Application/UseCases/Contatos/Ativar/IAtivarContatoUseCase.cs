namespace ProverContatos.Application.UseCases.Contatos.Ativar;

public interface IAtivarContatoUseCase
{
    Task ExecutarAsync(Guid id);
}