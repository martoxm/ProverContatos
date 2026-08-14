namespace ProverContatos.Application.UseCases.Contatos.Ativar;

public interface IAtivarContatoUseCase
{
    Task ExecutarAsync(long id);
}