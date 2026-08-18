using FluentAssertions;
using FluentValidation;
using ProverContatos.Domain.Entities;
using ProverContatos.Domain.Enums;

namespace ProverContatos.Tests.Entities;

public class ContatoTests
{
    private static readonly DateOnly _dataNascimentoValida = new(1997, 7, 29);

    [Fact]
    public void Criar_DeveIniciarComoAtivo()
    {
        var contato = Contato.Criar("Gabriel", _dataNascimentoValida, Sexo.Masculino);

        contato.Ativo.Should().BeTrue();
    }

    [Fact]
    public void Criar_DeveLancarExcecao_QuandoNomeForVazio()
    {
        var acao = () => Contato.Criar(string.Empty, _dataNascimentoValida, Sexo.Masculino);

        acao.Should().Throw<ValidationException>()
            .WithMessage("*O nome é obrigatório*");
    }

    [Fact]
    public void Criar_DeveLancarExcecao_QuandoContatoForMenorDeIdade()
    {
        var menorDeIdade = DateOnly.FromDateTime(DateTime.Today).AddYears(-17);

        var acao = () => Contato.Criar("Gabriel", menorDeIdade, Sexo.Masculino);

        acao.Should().Throw<ValidationException>()
            .WithMessage("*O contato deve ser maior de idade*");
    }

    [Fact]
    public void Criar_DeveLancarExcecao_QuandoIdadeForZero()
    {
        var acao = () => Contato.Criar("Gabriel", DateOnly.FromDateTime(DateTime.Today), Sexo.Masculino);

        acao.Should().Throw<ValidationException>()
            .WithMessage("*A idade do contato não pode ser zero*");
    }

    [Fact]
    public void Desativar_DeveAlterarStatusParaInativo()
    {
        var contato = Contato.Criar("Gabriel", _dataNascimentoValida, Sexo.Masculino);

        contato.Desativar();

        contato.Ativo.Should().BeFalse();
    }

    [Fact]
    public void Atualizar_DeveLancarExcecao_QuandoDataNascimentoForFutura()
    {
        var contato = Contato.Criar("Gabriel", _dataNascimentoValida, Sexo.Masculino);
        var dataFutura = DateOnly.FromDateTime(DateTime.Today).AddDays(1);

        var acao = () => contato.Atualizar("Gabriel", dataFutura, Sexo.Masculino);

        acao.Should().Throw<ValidationException>()
            .WithMessage("*A data de nascimento não pode ser maior que a data atual*");
    }
}