using FluentAssertions;
using Moq;
using ProverContatos.Application.UseCases.Contatos.Desativar;
using ProverContatos.Domain.Entities;
using ProverContatos.Domain.Enums;
using ProverContatos.Domain.Repositories;
using ProverContatos.Exception.ExceptionsBase;

namespace ProverContatos.Tests.UseCases.Contatos.Desativar;

public class DesativarContatoUseCaseTests
{
    [Fact]
    public async Task ExecutarAsync_DeveDesativarContato_QuandoEncontrado()
    {
        var repository = new Mock<IContatoRepository>();
        var unityOfWork = new Mock<IUnityOfWork>();

        var contato = new Contato
        {
            Id = 1,
            Nome = "João",
            DataNascimento = new DateOnly(1991, 3, 20),
            Sexo = Sexo.Masculino,
            Ativo = true
        };

        repository.Setup(r => r.BuscarAtivosPorIdAsync(1)).ReturnsAsync(contato);

        var useCase = new DesativarContatoUseCase(repository.Object, unityOfWork.Object);

        await useCase.ExecutarAsync(1);

        contato.Ativo.Should().BeFalse();
        repository.Verify(r => r.Atualizar(contato), Times.Once);
        unityOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecutarAsync_DeveLancarNotFound_QuandoNaoEncontrado()
    {
        var repository = new Mock<IContatoRepository>();
        var unityOfWork = new Mock<IUnityOfWork>();

        repository.Setup(r => r.BuscarAtivosPorIdAsync(1)).ReturnsAsync((Contato?)null);

        var useCase = new DesativarContatoUseCase(repository.Object, unityOfWork.Object);

        var act = async () => await useCase.ExecutarAsync(1);

        await act.Should().ThrowAsync<NotFoundException>();
    }
}