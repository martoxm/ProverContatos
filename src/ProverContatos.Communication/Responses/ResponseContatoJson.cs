using ProverContatos.Domain.Enums;

namespace ProverContatos.Communication.Responses;

public class ResponseContatoJson
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public Sexo Sexo { get; set; }
    public int Idade { get; set; }
    public bool Ativo { get; set; }
}