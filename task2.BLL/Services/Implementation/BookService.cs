using AutoMapper;
using task2.Common.Dtos;
using task2.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using task2.DAL.Entities;
using task2.BLL.Services.Abstration;

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
        var books = _context.Books
            .Include(x => x.Reviews)
            .Include(x => x.Ratings)
            .AsEnumerable()
            .Select(x => _mapper.Map<BookDto>(x));

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
    public async Task RateABook(int id,NewScoreDto dto)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if(book is null)
        {
            throw new NullReferenceException(nameof(book));
        }

        var rating = _mapper.Map<Rating>(dto);
        await _context.Ratings.AddAsync(rating);
        book.Ratings.Add(rating);
        await _context.SaveChangesAsync();
    }

    public async Task<int> SaveReview(int id,NewReviewDto dto)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (book is null)
        {
            throw new NullReferenceException(nameof(book));
        }

        var review = _mapper.Map<Review>(dto);
        await _context.Reviews.AddAsync(review);
        book.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return book.Id;
    }

}
