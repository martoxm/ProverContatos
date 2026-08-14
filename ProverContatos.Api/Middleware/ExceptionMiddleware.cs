using System.Net;
using System.Text.Json;
using ProverContatos.Communication.Responses;
using ProverContatos.Exception.ExceptionsBase;

namespace ProverContatos.Api.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ProverException ex)
        {
            await TratarProverExceptionAsync(context, ex);
        }
        catch (System.Exception)
        {
            await TratarErroDesconhecidoAsync(context);
        }
    }

    private static async Task TratarProverExceptionAsync(HttpContext context, ProverException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)ex.StatusCode;

        var response = new ResponseErroJson(ex.Errors);
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static async Task TratarErroDesconhecidoAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new ResponseErroJson("Ocorreu um erro inesperado. Tente novamente mais tarde.");
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}