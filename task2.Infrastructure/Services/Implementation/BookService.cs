using AutoMapper;
using task2.Infrastructure.Dtos;
using task2.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using task2.DAL.Entities;
using task2.Infrastructure.Services.Abstration;

namespace task2.Infrastructure.Services.Implementation;

public class BookService : ServiceBase, IBookService
{
    public BookService(IMapper mapper, BooksContext context) : base(mapper, context)
    {
    }

    public async Task Delete(int id)
    {
        var model = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (model is null)
        {
            throw new NullReferenceException(nameof(model));
        }

        _context.Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<BookDto>> Get(string orderPropName)
    {
        var books = _context.Books
            .Include(x => x.Reviews)
            .Include(x => x.Ratings);

        var orderedBooks = orderPropName switch
        {
            nameof(BookDto.Author) => books.OrderBy(x => x.Author),
            nameof(BookDto.Title) => books.OrderByDescending(x => x.Title),
            _ => throw new InvalidDataException("Wrong order prop name"),
        };

        var orderedList = await orderedBooks.ToListAsync();
        return _mapper.Map<IEnumerable<BookDto>>(orderedList);
    }

    public async Task<IEnumerable<BookDto>> GetBooksWithHighRatings(string? genre)
    {
        var result = await _context
                .Books
                .Where(x => x.Reviews.Count >= 3)
                .Where(x => genre == null || x.Genre == genre)
                .OrderByDescending(x => x.Ratings.Count)
                .Take(10)
                .OrderBy(x => x.Ratings.Count)
                .ToListAsync();

        return _mapper.Map<IEnumerable<BookDto>>(result);
    }

    public async Task<BookWithReviewsDto> GetDetails(int id)
    {
        var book = await _context.Books
            .Include(x => x.Ratings)
            .Include(x => x.Reviews)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (book is null)
        {
            throw new NullReferenceException(nameof(book));
        }

        return _mapper.Map<BookWithReviewsDto>(book);
    }


    public async Task<int> Save(NewBookDto dto)
    {
        var model = _mapper.Map<Book>(dto);
        if (dto.Id is null)
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
    public async Task RateABook(NewScoreDto dto)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == dto.BookId);
        if (book is null)
        {
            throw new NullReferenceException(nameof(book));
        }

        var rating = _mapper.Map<Rating>(dto);
        await _context.Ratings.AddAsync(rating);
        book.Ratings.Add(rating);
        await _context.SaveChangesAsync();
    }

    public async Task<int> SaveReview(NewReviewDto dto)
    {
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == dto.BookId);
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
