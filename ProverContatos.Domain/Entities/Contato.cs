using ProverContatos.Domain.Enums;

namespace ProverContatos.Domain.Entities;

public class Contato : EntityBase
{
    public string Nome { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public Sexo Sexo { get; set; }
    public bool Ativo { get; set; } = true;

    public int Idade => CalcularIdade();

    private int CalcularIdade()
    {
        var hoje = DateOnly.FromDateTime(DateTime.Today);
        var idade = hoje.Year - DataNascimento.Year;
        if (DataNascimento > hoje.AddYears(-idade))
            idade--;
        return idade;
    }
}