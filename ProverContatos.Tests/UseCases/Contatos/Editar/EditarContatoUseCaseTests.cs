using FluentAssertions;
using Moq;
using ProverContatos.Application.UseCases.Contatos.Editar;
using ProverContatos.Communication.Requests;
using ProverContatos.Domain.Entities;
using ProverContatos.Domain.Enums;
using ProverContatos.Domain.Repositories;
using ProverContatos.Exception.ExceptionsBase;

namespace ProverContatos.Tests.UseCases.Contatos.Editar;

public class EditarContatoUseCaseTests
{
    [Fact]
    public async Task ExecutarAsync_DeveEditarContato_QuandoContatoAtivoExistir()
    {
        var repository = new Mock<IContatoRepository>();
        var unityOfWork = new Mock<IUnityOfWork>();

        var contato = new Contato
        {
            Id = 1,
            Nome = "Antigo Nome",
            DataNascimento = new DateOnly(1990, 1, 1),
            Sexo = Sexo.Masculino,
            Ativo = true
        };

        repository.Setup(r => r.BuscarAtivosPorIdAsync(1)).ReturnsAsync(contato);

        var useCase = new EditarContatoUseCase(repository.Object, unityOfWork.Object);

        var request = new RequestEditarContatoJson
        {
            Nome = "Novo Nome",
            DataNascimento = new DateOnly(1992, 2, 2),
            Sexo = Sexo.Outro
        };

        await useCase.ExecutarAsync(1, request);

        contato.Nome.Should().Be("Novo Nome");
        contato.DataNascimento.Should().Be(new DateOnly(1992, 2, 2));
        contato.Sexo.Should().Be(Sexo.Outro);

        repository.Verify(r => r.Atualizar(contato), Times.Once);
        unityOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecutarAsync_DeveLancarNotFound_QuandoContatoNaoExistir()
    {
        var repository = new Mock<IContatoRepository>();
        var unityOfWork = new Mock<IUnityOfWork>();

        repository.Setup(r => r.BuscarAtivosPorIdAsync(1)).ReturnsAsync((Contato?)null);

        var useCase = new EditarContatoUseCase(repository.Object, unityOfWork.Object);

        var request = new RequestEditarContatoJson
        {
            Nome = "Novo Nome",
            DataNascimento = new DateOnly(1992, 2, 2),
            Sexo = Sexo.Feminino
        };

        var act = async () => await useCase.ExecutarAsync(1, request);

        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task ExecutarAsync_DeveLancarExcecao_QuandoRequestForInvalido()
    {
        var repository = new Mock<IContatoRepository>();
        var unityOfWork = new Mock<IUnityOfWork>();

        var useCase = new EditarContatoUseCase(repository.Object, unityOfWork.Object);

        var request = new RequestEditarContatoJson
        {
            Nome = "",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today.AddYears(-10)),
            Sexo = Sexo.Masculino
        };

        var act = async () => await useCase.ExecutarAsync(1, request);

        var exception = await act.Should().ThrowAsync<ProverException>();
        exception.Which.Errors.Should().Contain("O nome é obrigatório.");
    }
}