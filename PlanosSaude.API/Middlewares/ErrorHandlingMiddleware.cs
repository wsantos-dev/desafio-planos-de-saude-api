using System.Net;
using System.Text.Json;
using PlanosSaude.API.Errors;
using PlanosSaude.API.Errors.Exceptions;

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
            catch (BaseException ex)
            {
                await HandleCustomException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGenericException(context, ex);
            }
        }

        private static async Task HandleGenericException(
            HttpContext context,
            Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = new ErrorResponse(
                "Erro interno no servidor.",
                ex.Message,
                DateTime.UtcNow
            );

            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }

        private static async Task HandleCustomException(
            HttpContext context,
            BaseException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;

            var error = new ErrorResponse(
                ex.Message,
                null,
                DateTime.UtcNow
            );

            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
    }
}