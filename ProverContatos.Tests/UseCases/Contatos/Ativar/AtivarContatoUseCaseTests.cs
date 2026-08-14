using FluentAssertions;
using Moq;
using ProverContatos.Application.UseCases.Contatos.Ativar;
using ProverContatos.Domain.Entities;
using ProverContatos.Domain.Enums;
using ProverContatos.Domain.Repositories;
using ProverContatos.Exception.ExceptionsBase;

namespace ProverContatos.Tests.UseCases.Contatos.Ativar;

public class AtivarContatoUseCaseTests
{
    [Fact]
    public async Task ExecutarAsync_DeveAtivarContato_QuandoContatoEstiverInativo()
    {
        var repository = new Mock<IContatoRepository>();
        var unityOfWork = new Mock<IUnityOfWork>();

        var contato = new Contato
        {
            Id = 1,
            Nome = "Carlos",
            DataNascimento = new DateOnly(1990, 10, 10),
            Sexo = Sexo.Masculino,
            Ativo = false
        };

        repository.Setup(r => r.BuscarPorIdAsync(1)).ReturnsAsync(contato);// Configura o mock do repositório para retornar o contato inativo quando o método BuscarPorIdAsync for chamado com o ID 1.


        var useCase = new AtivarContatoUseCase(repository.Object, unityOfWork.Object);

        await useCase.ExecutarAsync(1);

        contato.Ativo.Should().BeTrue();
        repository.Verify(r => r.Atualizar(contato), Times.Once);
        unityOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecutarAsync_DeveLancarExcecao_QuandoContatoJaEstiverAtivo()
    {
        var repository = new Mock<IContatoRepository>();
        var unityOfWork = new Mock<IUnityOfWork>();

        var contato = new Contato
        {
            Id = 1,
            Nome = "Carlos",
            DataNascimento = new DateOnly(1990, 10, 10),
            Sexo = Sexo.Masculino,
            Ativo = true
        };

        repository.Setup(r => r.BuscarPorIdAsync(1)).ReturnsAsync(contato);

        var useCase = new AtivarContatoUseCase(repository.Object, unityOfWork.Object);

        var act = async () => await useCase.ExecutarAsync(1);

        await act.Should().ThrowAsync<ProverException>()
            .WithMessage("Contato já está ativo.");
    }
}