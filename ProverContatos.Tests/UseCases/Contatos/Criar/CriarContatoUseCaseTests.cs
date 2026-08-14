using FluentAssertions;
using Moq;
using ProverContatos.Application.UseCases.Contatos.Criar;
using ProverContatos.Communication.Requests;
using ProverContatos.Domain.Entities;
using ProverContatos.Domain.Enums;
using ProverContatos.Domain.Repositories;
using ProverContatos.Exception.ExceptionsBase;

namespace ProverContatos.Tests.UseCases.Contatos.Criar;

public class CriarContatoUseCaseTests
{
    [Fact]
    public async Task ExecutarAsync_DeveCriarContato_ComDadosValidos()
    {
        var repository = new Mock<IContatoRepository>();
        var unityOfWork = new Mock<IUnityOfWork>();

        repository
            .Setup(r => r.AdicionarAsync(It.IsAny<Contato>())) // diz o que deve acontecer quando o método AdicionarAsync for chamado.
            .Callback<Contato>(contato => contato.Id = 1)//quando o método for chamado, ele altera o Id do contato para 1.
            .Returns(Task.CompletedTask);//o método retorna uma Task já concluída (como se tivesse rodado normalmente).

        var useCase = new CriarContatoUseCase(repository.Object, unityOfWork.Object);//Cria a instância do use case que será testado.

        var request = new RequestCriarContatoJson//Cria o objeto de entrada do use case.
        {
            Nome = "Gabriel Martorelli",
            DataNascimento = new DateOnly(1997, 7, 29),
            Sexo = Sexo.Masculino
        };

        var response = await useCase.ExecutarAsync(request);//Chama o método principal do caso de uso.

        response.Should().NotBeNull();
        response.Id.Should().Be(1);
        response.Nome.Should().Be(request.Nome);
        response.DataNascimento.Should().Be(request.DataNascimento);
        response.Sexo.Should().Be(request.Sexo);
        response.Ativo.Should().BeTrue();
        response.Idade.Should().BeGreaterThanOrEqualTo(18);

        repository.Verify(r => r.AdicionarAsync(It.IsAny<Contato>()), Times.Once);
        unityOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }

    [Fact]
    public async Task ExecutarAsync_DeveLancarExcecao_QuandoNomeForVazio()
    {
        var repository = new Mock<IContatoRepository>();
        var unityOfWork = new Mock<IUnityOfWork>();

        var useCase = new CriarContatoUseCase(repository.Object, unityOfWork.Object);

        var request = new RequestCriarContatoJson
        {
            Nome = string.Empty,
            DataNascimento = new DateOnly(1997, 7, 29),
            Sexo = Sexo.Masculino
        };

        var act = async () => await useCase.ExecutarAsync(request);//Cria uma função assíncrona que chama o método ExecutarAsync do use case com o request inválido.

        var exception = await act.Should().ThrowAsync<ProverException>();//verifica se a exceção lançada é do tipo ProverException
        exception.Which.Errors.Should().Contain("O nome é obrigatório.");//verifica se a mensagem de erro contém a mensagem esperada

        repository.Verify(r => r.AdicionarAsync(It.IsAny<Contato>()), Times.Never);
        unityOfWork.Verify(u => u.CommitAsync(), Times.Never);
    }

    [Fact]
    public async Task ExecutarAsync_DeveLancarExcecao_QuandoContatoForMenorDeIdade()
    {
        var repository = new Mock<IContatoRepository>();
        var unityOfWork = new Mock<IUnityOfWork>();

        var useCase = new CriarContatoUseCase(repository.Object, unityOfWork.Object);

        var request = new RequestCriarContatoJson
        {
            Nome = "Contato Menor",
            DataNascimento = DateOnly.FromDateTime(DateTime.Today.AddYears(-17)),
            Sexo = Sexo.Outro
        };

        var act = async () => await useCase.ExecutarAsync(request);

        var exception = await act.Should().ThrowAsync<ProverException>();
        exception.Which.Errors.Should().Contain("O contato deve ser maior de idade (mínimo 18 anos).");

        repository.Verify(r => r.AdicionarAsync(It.IsAny<Contato>()), Times.Never);
        unityOfWork.Verify(u => u.CommitAsync(), Times.Never);
    }
}