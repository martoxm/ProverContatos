using FluentAssertions;
using ProverContatos.Domain.Entities;
using ProverContatos.Domain.Enums;

namespace ProverContatos.Tests.Entities;

public class ContatoTests
{
    [Fact]
    public void CriarContato_DeveIniciarComoAtivo()
    {
        var contato = new Contato(
            "Gabriel",
            new DateOnly(1997, 7, 29),
            Sexo.Masculino);

        contato.Ativo.Should().BeTrue();
    }

    [Fact]
    public void Desativar_DeveAlterarStatusParaInativo()
    {
        var contato = new Contato(
            "Gabriel",
            new DateOnly(1997, 7, 29),
            Sexo.Masculino);

        contato.Desativar();

        contato.Ativo.Should().BeFalse();
    }

    [Fact]
    public void Ativar_DeveAlterarStatusParaAtivo()
    {
        var contato = new Contato(
            "Gabriel",
            new DateOnly(1997, 7, 29),
            Sexo.Masculino);

        contato.Desativar();
        contato.Ativar();

        contato.Ativo.Should().BeTrue();
    }

    [Fact]
    public void Atualizar_DeveAlterarDadosDoContato()
    {
        var contato = new Contato(
            "Nome Antigo",
            new DateOnly(1990, 1, 1),
            Sexo.Masculino);

        contato.Atualizar(
            "Nome Novo",
            new DateOnly(1992, 2, 2),
            Sexo.Outro);

        contato.Nome.Should().Be("Nome Novo");
        contato.DataNascimento.Should()
            .Be(new DateOnly(1992, 2, 2));
        contato.Sexo.Should().Be(Sexo.Outro);
    }

    [Fact]
    public void Idade_DeveSerCalculadaComBaseNaDataDeNascimento()
    {
        var dataNascimento = DateOnly
            .FromDateTime(DateTime.Today)
            .AddYears(-30);

        var contato = new Contato(
            "Gabriel",
            dataNascimento,
            Sexo.Masculino);

        contato.Idade.Should().Be(30);
    }

    [Fact]
    public void CalcularIdade_DeveRetornarIdadeCorreta()
    {
        var hoje = DateOnly.FromDateTime(DateTime.Today);
        var dataNascimento = hoje.AddYears(-30);

        var idade = Contato.CalcularIdade(dataNascimento);

        idade.Should().Be(30);
    }

    [Fact]
    public void DataNascimentoEhValida_DeveRetornarFalse_QuandoDataForFutura()
    {
        // Arrange
        var dataNascimento = DateOnly
            .FromDateTime(DateTime.Today)
            .AddDays(1);

        // Act
        var resultado = Contato.DataNascimentoEhValida(
            dataNascimento);

        // Assert
        resultado.Should().BeFalse();
    }

    [Fact]
    public void EhMaiorDeIdade_DeveRetornarTrue_QuandoContatoTiver18Anos()
    {
        // Arrange
        var dataNascimento = DateOnly
            .FromDateTime(DateTime.Today)
            .AddYears(-18);

        // Act
        var resultado = Contato.EhMaiorDeIdade(
            dataNascimento);

        // Assert
        resultado.Should().BeTrue();
    }
}