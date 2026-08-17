using FluentValidation;
using ProverContatos.Domain.Enums;

namespace ProverContatos.Domain.Entities;

public class Contato : EntityBase
{
    public string Nome { get; private set; } = string.Empty;
    public DateOnly DataNascimento { get; private set; }
    public Sexo Sexo { get; private set; }
    public bool Ativo { get; private set; } = true;

    public int Idade => CalcularIdade(DataNascimento);

    private Contato() { }

    public Contato(
        string nome,
        DateOnly dataNascimento,
        Sexo sexo)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        Sexo = sexo;

        new ContatoValidator().ValidateAndThrow(this);
    }

    public void Atualizar(
        string nome,
        DateOnly dataNascimento,
        Sexo sexo)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        Sexo = sexo;

        new ContatoValidator().ValidateAndThrow(this);
    }

    public void Ativar() => Ativo = true;

    public void Desativar() => Ativo = false;



    public static int CalcularIdade(DateOnly dataNascimento)
    {
        var hoje = DateOnly.FromDateTime(DateTime.Today);
        var idade = hoje.Year - dataNascimento.Year;

        if (dataNascimento > hoje.AddYears(-idade))
            idade--;

        return idade;
    }

    public static bool DataNascimentoEhValida(DateOnly dataNascimento)
    {
        var hoje = DateOnly.FromDateTime(DateTime.Today);
        return dataNascimento != DateOnly.MinValue && dataNascimento <= hoje;
    }

    public static bool EhMaiorDeIdade(DateOnly dataNascimento)
    {
        return DataNascimentoEhValida(dataNascimento)
            && CalcularIdade(dataNascimento) >= 18;
    }

    public static bool IdadeEhDiferenteDeZero(
       DateOnly dataNascimento)
    {
        return DataNascimentoEhValida(dataNascimento)
            && CalcularIdade(dataNascimento) != 0;
    }
}