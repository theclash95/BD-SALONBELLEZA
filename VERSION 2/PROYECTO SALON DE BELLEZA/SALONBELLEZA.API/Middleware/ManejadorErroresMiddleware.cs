using System.Net;
using System.Text.Json;
namespace SALONBELLEZA.API.Middlewares
{
    public class ManejadorErroresMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorErroresMiddleware> _logger;

        public ManejadorErroresMiddleware(RequestDelegate next, ILogger<ManejadorErroresMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error no controlado.");
                await ManejarExcepcionAsync(context, ex);
            }
        }

        private static Task ManejarExcepcionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest, // 400
                KeyNotFoundException => (int)HttpStatusCode.NotFound, // 404
                _ => (int)HttpStatusCode.InternalServerError // 500
            };

            context.Response.StatusCode = statusCode;

            var respuesta = new
            {
                Status = statusCode,
                Mensaje = exception.Message,
                Detalle = exception.InnerException?.Message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(respuesta));
        }
    }
}