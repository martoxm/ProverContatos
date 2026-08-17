using System.Net;

namespace ProverContatos.Exception.ExceptionsBase;

public class ProverException : SystemException
{
    public IList<string> Errors { get; }// Lista de mensagens de erro associadas à exceção.
    public HttpStatusCode StatusCode { get; }// Código de status HTTP associado à exceção.

    public ProverException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message)
    {
        Errors = [message];
        StatusCode = statusCode;
    }// Construtor que aceita uma única mensagem de erro e um código de status HTTP opcional (padrão é BadRequest).

    public ProverException(IList<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(string.Join(", ", errors))
    {
        Errors = errors;
        StatusCode = statusCode;
    }
}