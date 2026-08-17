using ProverContatos.Domain.Enums;

namespace ProverContatos.Communication.Requests;

public class RequestCriarContatoJson
{
    public string Nome { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public Sexo Sexo { get; set; }
}