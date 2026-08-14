using System.Net;

namespace ProverContatos.Exception.ExceptionsBase;

public class NotFoundException(string message) : ProverException(message, HttpStatusCode.NotFound)
{
}