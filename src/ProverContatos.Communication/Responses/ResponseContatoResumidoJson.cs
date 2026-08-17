using ProverContatos.Domain.Enums;

namespace ProverContatos.Communication.Responses;

public class ResponseContatoResumidoJson
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public Sexo Sexo { get; set; }
}