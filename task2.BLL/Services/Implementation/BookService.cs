using AutoMapper;
using task2.Common.Dtos;
using task2.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace task2.BLL.Services.Implementation;

public class BookService : Abstration.ServiceBase, Abstration.IBookService
{
    public BookService(IMapper mapper, BooksContext context) : base(mapper, context)
    {
    }

    public async Task Delete(int id)
    {
        var model = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if(model is null)
        {
            throw new NullReferenceException(nameof(model));    
        }

        _context.Remove(model);
        await _context.SaveChangesAsync();
    }

    public IEnumerable<BookDto> Get(string orderPropName)
    {
        var g = _context.Books
            .Include(x => x.Reviews)
            .Include(x => x.Ratings)
            .AsEnumerable()
            .ToList();

        var books = g.Select(x => _mapper.Map<BookDto>(x));
        return orderPropName switch
        {
            nameof(BookDto.Author) => books.OrderBy(x => x.Author),
            nameof(BookDto.Title) => books.OrderBy(x => x.Title),
            _ => throw new InvalidDataException("wRONG ORDERPROPNAME"),
        };
    }

    public IEnumerable<BookDto> GetBooksWithHighRatings(string? genre) => _context
         .Books
         .Where(x => x.Reviews.Count >= 3)
         .Where(x => genre == null || x.Genre == genre)
         .AsEnumerable()
         .OrderByDescending(x => x.Ratings.Count)
         .Take(10)
         .OrderBy(x => x.Ratings.Count)
         .Select(x => _mapper.Map<BookDto>(x));

    public async Task<BookWithReviewsDto> GetDetails(int id)
    {
        var book = await _context.Books
            .Include(x => x.Ratings)
            .Include(x => x.Reviews)
            .FirstOrDefaultAsync(x => x.Id == id);
        if(book is null)
        {
            throw new NullReferenceException(nameof(book));
        }

        return _mapper.Map<BookWithReviewsDto>(book);
    }

    public async Task<int> Save(NewBookDto dto)
    {
        var model = _mapper.Map<DAL.Entities.Book>(dto);
        if(dto.Id is null)
        {
            await _context.Books.AddAsync(model);
        }
        else
        {
            _context.Update(model);
        }

        await _context.SaveChangesAsync();
        return model.Id;

    }
}
