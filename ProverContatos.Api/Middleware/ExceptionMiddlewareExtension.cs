namespace ProverContatos.Api.Middleware;

public static class ExceptionMiddlewareExtension
{
    public static void UseExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}