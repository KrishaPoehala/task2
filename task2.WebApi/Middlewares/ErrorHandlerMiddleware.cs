using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace task2.WebApi.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.StatusCode = ex switch
            {
                InvalidDataException => (int)System.Net.HttpStatusCode.BadRequest,
                NullReferenceException => (int)System.Net.HttpStatusCode.NotFound,
            };

            var result = System.Text.Json.JsonSerializer.Serialize(new {message = ex.Message});
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(result));
        }
    }
}
