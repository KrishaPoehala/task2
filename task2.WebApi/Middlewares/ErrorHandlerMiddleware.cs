using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace task2.WebApi.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerFactory _factory;

    public ErrorHandlerMiddleware(RequestDelegate next, ILoggerFactory factory)
    {
        _next = next;
        _factory = factory;
    }

    public async Task Invoke(HttpContext context)
    {
        var _logger = _factory.CreateLogger<ErrorHandlerMiddleware>();
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {

            _logger.LogError(ex.Message);
            var response = context.Response;
            response.StatusCode = ex switch
            {
                InvalidDataException => (int)System.Net.HttpStatusCode.BadRequest,
                NullReferenceException => (int)System.Net.HttpStatusCode.NotFound,
            };

            var result = System.Text.Json.JsonSerializer.Serialize(new {message = ex.Message});
            await context.Response.WriteAsync(result);
        }
    }
}
