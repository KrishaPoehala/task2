using Microsoft.AspNetCore.Mvc;
using task2.BLL.Services.Abstration;

namespace task2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BLL.Services.Abstration.IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Common.Dtos.BookDto>> Get([FromQuery] string opt)
        {
            return Ok(_bookService.Get(opt));
        }

        [HttpGet]
        [Route("recommended")]
        public ActionResult<IEnumerable<Common.Dtos.BookDto>> GetRecommended([FromQuery] string? genre)
        {
            return Ok(_bookService.GetBooksWithHighRatings(genre));
        }
    }
}
