namespace ProverContatos.Communication.Responses;

public class ResponseErroJson
{
    public IList<string> Errors { get; set; }

    public ResponseErroJson(string message)
    {
        Errors = new List<string> { message };
    }

    public ResponseErroJson(IList<string> errors)
    {
        Errors = errors;
    }
}