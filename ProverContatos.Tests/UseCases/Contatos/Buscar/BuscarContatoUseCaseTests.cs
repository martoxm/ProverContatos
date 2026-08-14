using FluentAssertions;
using Moq;
using ProverContatos.Application.UseCases.Contatos.Buscar;
using ProverContatos.Domain.Entities;
using ProverContatos.Domain.Enums;
using ProverContatos.Domain.Repositories;
using ProverContatos.Exception.ExceptionsBase;

namespace ProverContatos.Tests.UseCases.Contatos.Buscar;

public class BuscarContatoUseCaseTests
{
    [Fact]
    public async Task ExecutarAsync_DeveRetornarContato_QuandoEncontradoEAtivo()
    {
        var repository = new Mock<IContatoRepository>();

        repository.Setup(r => r.BuscarAtivosPorIdAsync(1)).ReturnsAsync(new Contato
        {
            Id = 1,
            Nome = "Maria",
            DataNascimento = new DateOnly(1990, 5, 10),
            Sexo = Sexo.Feminino,
            Ativo = true
        });

        var useCase = new BuscarContatoUseCase(repository.Object);

        var response = await useCase.ExecutarAsync(1);

        response.Id.Should().Be(1);
        response.Nome.Should().Be("Maria");
        response.Ativo.Should().BeTrue();
    }

    [Fact]
    public async Task ExecutarAsync_DeveLancarNotFound_QuandoNaoEncontrado()
    {
        var repository = new Mock<IContatoRepository>();
        repository.Setup(r => r.BuscarAtivosPorIdAsync(1)).ReturnsAsync((Contato?)null);

        var useCase = new BuscarContatoUseCase(repository.Object);

        var act = async () => await useCase.ExecutarAsync(1);

        await act.Should().ThrowAsync<NotFoundException>();
    }
}