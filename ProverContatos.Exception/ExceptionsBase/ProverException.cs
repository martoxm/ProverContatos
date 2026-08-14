using System.Net;

namespace ProverContatos.Exception.ExceptionsBase;

public class ProverException : SystemException
{
    public IList<string> Errors { get; }
    public HttpStatusCode StatusCode { get; }

    public ProverException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message)
    {
        Errors = [message];
        StatusCode = statusCode;
    }

    public ProverException(IList<string> errors, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(string.Join(", ", errors))
    {
        Errors = errors;
        StatusCode = statusCode;
    }
}