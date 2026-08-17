namespace ProverContatos.Application.UseCases.Contatos.Excluir;

public interface IExcluirContatoUseCase
{
    Task ExecutarAsync(Guid id);
}