using Microsoft.AspNetCore.Mvc;
using task2.BLL.Services.Abstration;
using task2.Common.Dtos;
namespace task2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BLL.Services.Abstration.IBookService _bookService;
        private readonly IConfiguration _configuration;
        public BooksController(IBookService bookService, IConfiguration configuration)
        {
            _bookService = bookService;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> Get([FromQuery] string opt = "Title")
        {
            return Ok(_bookService.Get(opt));
        }

        [HttpGet]
        [Route("recommended")]
        public ActionResult<IEnumerable<BookDto>> GetRecommended([FromQuery] string? genre)
        {
            return Ok(_bookService.GetBooksWithHighRatings(genre));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<BookWithReviewsDto>>> GetBookDetails(int id)
        {
            return Ok(await _bookService.GetDetails(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete(int id,[FromQuery] string secret)
        {
            if(_configuration.GetSection("SecretKey").Value == secret)
            {
                await _bookService.Delete(id);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("save")]
        public async Task<ActionResult<int>> PostBook(NewBookDto dto)
        {
            return Ok(await _bookService.Save(dto));
        } 
    }
}
