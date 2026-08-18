using System.Net;

namespace ProverContatos.Exception.ExceptionsBase;

public class ProverException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    : System.Exception(message)
{
    public IList<string> Errors { get; } = [message];
    public HttpStatusCode StatusCode { get; } = statusCode;
}