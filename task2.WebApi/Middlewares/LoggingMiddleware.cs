namespace task2.WebApi.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerFactory _factory;
    

    public LoggingMiddleware(RequestDelegate next, ILoggerFactory factory)
    {
        _next = next;
        _factory = factory;
    }
    public async Task Invoke(HttpContext context)
    {
        var _logger = _factory.CreateLogger<ErrorHandlerMiddleware>();
        _logger.LogInformation(message: context.Request.Method);
        foreach (var item in context.Request.Headers)
        {
            _logger.LogInformation(item.Key + " " + item.Value);
        }

        await _next(context);
    }
}
