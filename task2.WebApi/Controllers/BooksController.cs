using MediatR;
using Microsoft.AspNetCore.Mvc;
using task2.BLL.Commands;
using task2.BLL.Queries;

namespace task2.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IConfiguration _configuration;
    public BooksController(IConfiguration configuration, ISender sender)
    {
        _configuration = configuration;
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string opt = "Title")
    {
        var request = new GetOrderedBooksQuery() { OrderPropName = opt };
        return Ok(await _sender.Send(request));
    }

    [HttpGet]
    [Route("recommended")]
    public async Task<IActionResult> GetRecommended([FromQuery] string? genre)
    {
        var request = new GetBooksWithHighRatingsQuery() { Genre = genre };
        return Ok(await _sender.Send(request));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetBookDetails(int id)
    {
        var request = new GetDetailsQuery() { Id = id };
        return Ok(await _sender.Send(request));
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id, [FromQuery] string secret)
    {
        if (_configuration.GetSection("SecretKey").Value == secret)
        {
            var request = new DeleteBookCommand() { Id = id };
            await _sender.Send(request);
            return Ok();
        }

        return BadRequest("Secret key and provided value do not match");
    }

    [HttpPost]
    [Route("save")]
    public async Task<IActionResult> PostBook(SaveBookCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPost]
    [Route("review")]
    public async Task<IActionResult> SaveReview(SaveReviewCommand command)
    {
        return Ok(await _sender.Send(command));
    }

    [HttpPost]
    [Route("rate")]
    public async Task<IActionResult> RateABook(RateABookCommand command)
    {
        await _sender.Send(command);
        return Ok();
    }
}
