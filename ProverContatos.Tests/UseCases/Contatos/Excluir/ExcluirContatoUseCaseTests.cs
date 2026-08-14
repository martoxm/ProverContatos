using FluentAssertions;
using Moq;
using ProverContatos.Application.UseCases.Contatos.Excluir;
using ProverContatos.Domain.Entities;
using ProverContatos.Domain.Enums;
using ProverContatos.Domain.Repositories;
using ProverContatos.Exception.ExceptionsBase;

namespace ProverContatos.Tests.UseCases.Contatos.Excluir;

public class ExcluirContatoUseCaseTests
{
    [Fact]
    public async Task ExecutarAsync_DeveExcluirContato_QuandoEncontrado()
    {
        var repository = new Mock<IContatoRepository>();
        var unityOfWork = new Mock<IUnityOfWork>();

        var contato = new Contato
        {
            Id = 1,
            Nome = "Fernanda",
            DataNascimento = new DateOnly(1989, 8, 8),
            Sexo = Sexo.Feminino,
            Ativo = true
        };

        repository.Setup(r => r.BuscarPorIdAsync(1)).ReturnsAsync(contato);

        var useCase = new ExcluirContatoUseCase(repository.Object, unityOfWork.Object);

        await useCase.ExecutarAsync(1);

        repository.Verify(r => r.Excluir(contato), Times.Once);
        unityOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecutarAsync_DeveLancarNotFound_QuandoContatoNaoExistir()
    {
        var repository = new Mock<IContatoRepository>();
        var unityOfWork = new Mock<IUnityOfWork>();

        repository.Setup(r => r.BuscarPorIdAsync(1)).ReturnsAsync((Contato?)null);

        var useCase = new ExcluirContatoUseCase(repository.Object, unityOfWork.Object);

        var act = async () => await useCase.ExecutarAsync(1);

        await act.Should().ThrowAsync<NotFoundException>();
    }
}