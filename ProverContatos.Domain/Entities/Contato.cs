using ProverContatos.Domain.Enums;

namespace ProverContatos.Domain.Entities;

public class Contato : EntityBase
{
    public string Nome { get; private set; } = string.Empty;
    public DateOnly DataNascimento { get; private set; }
    public Sexo Sexo { get; private set; }
    public bool Ativo { get; private set; } = true;

    public int Idade => CalcularIdade();

    private Contato()
    {
    }

    public Contato(
        string nome,
        DateOnly dataNascimento,
        Sexo sexo)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        Sexo = sexo;
        Ativo = true;
    }

    public void Atualizar(
        string nome,
        DateOnly dataNascimento,
        Sexo sexo)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        Sexo = sexo;
    }

    public void Ativar()
    {
        Ativo = true;
    }

    public void Desativar()
    {
        Ativo = false;
    }

    private int CalcularIdade()
    {
        var hoje = DateOnly.FromDateTime(DateTime.Today);

        var idade = hoje.Year - DataNascimento.Year;

        if (DataNascimento > hoje.AddYears(-idade))
        {
            idade--;
        }

        return idade;
    }
}