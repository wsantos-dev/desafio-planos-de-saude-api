using System.Net;
using System.Text.Json;
using PlanosSaude.API.Errors;

namespace PlanosSaude.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var error = new ErrorResponse(
                    "Erro interno no servidor.",
                    ex.Message,
                    DateTimeOffset.UtcNow
                );

                var json = JsonSerializer.Serialize(error);

                await context.Response.WriteAsync(json);
            }
        }
    }
}