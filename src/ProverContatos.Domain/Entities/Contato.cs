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

    private Contato()
    { }

    public static Contato Criar(
       string nome,
       DateOnly dataNascimento,
       Sexo sexo)
    {
        var contato = new Contato
        {
            Nome = nome,
            DataNascimento = dataNascimento,
            Sexo = sexo
        };

        Validar(contato);

        return contato;
    }

    public void Atualizar(
        string nome,
        DateOnly dataNascimento,
        Sexo sexo)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        Sexo = sexo;

        Validar(this);
    }

    public void Ativar() => Ativo = true;

    public void Desativar() => Ativo = false;

    private static void Validar(Contato contato)
    {
        new ContatoValidator().ValidateAndThrow(contato);
    }

    internal static int CalcularIdade(DateOnly dataNascimento)
    {
        var hoje = DateOnly.FromDateTime(DateTime.Today);
        var idade = hoje.Year - dataNascimento.Year;

        if (dataNascimento > hoje.AddYears(-idade))
            idade--;

        return idade;
    }

    internal static bool DataNascimentoEhValida(DateOnly dataNascimento)
    {
        var hoje = DateOnly.FromDateTime(DateTime.Today);
        return dataNascimento != DateOnly.MinValue && dataNascimento <= hoje;
    }

    internal static bool EhMaiorDeIdade(DateOnly dataNascimento)
    {
        return DataNascimentoEhValida(dataNascimento)
            && CalcularIdade(dataNascimento) >= 18;
    }

    internal static bool IdadeEhDiferenteDeZero(
       DateOnly dataNascimento)
    {
        return DataNascimentoEhValida(dataNascimento)
            && CalcularIdade(dataNascimento) != 0;
    }
}