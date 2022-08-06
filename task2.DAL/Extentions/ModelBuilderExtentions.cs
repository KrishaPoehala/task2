using Bogus;
using Microsoft.EntityFrameworkCore;
using task2.DAL.Entities;

namespace task2.DAL.Extentions;

public static class ModelBuilderExtentions
{
    public static void Seed(this ModelBuilder builder)
    {
        var books = GenerateBooks(25);
        var reviews = GenerateReviews(500, books);
        var ratings = GenerateRatings(100, books);
        builder.Entity<Book>().HasData(books);
        builder.Entity<Review>().HasData(reviews);
        builder.Entity<Rating>().HasData(ratings);
    }

    private static IEnumerable<Rating> GenerateRatings(int v, IEnumerable<Book> books)
    {
        var index = 1;
        var faker = new Faker<Rating>()
            .RuleFor(x => x.Id, f => ++index)
            .RuleFor(x => x.BookId, f => f.PickRandom(books).Id)
            .RuleFor(x => x.Score, f => f.Random.Int(1, 6));

        return faker.Generate(v);
    }

    private static IEnumerable<Review> GenerateReviews(int v, IEnumerable<Book> books)
    {
        var index = 1;
        var faker = new Faker<Review>()
            .RuleFor(x => x.Id, f => ++index)
            .RuleFor(x => x.Message, f => f.Lorem.Sentence(10))
            .RuleFor(x => x.Reviewer, f=>f.Name.FirstName())
            .RuleFor(x => x.BookId, f => f.PickRandom(books).Id);

        return faker.Generate(v);
    }

    private static IEnumerable<Book> GenerateBooks(int v)
    {
        var index = 1;
        var faker = new Faker<Book>()
            .RuleFor(x => x.Id, f => ++index)
            .RuleFor(x => x.Content, f => f.Lorem.Sentence(10))
            .RuleFor(x => x.Genre, f => f.Lorem.Sentence(5))
            .RuleFor(x => x.Title, f => f.Lorem.Sentence(5))
            .RuleFor(x => x.Cover, f => f.Lorem.Sentence(15))
            .RuleFor(x => x.Content, f => f.Image.Image());

        return faker.Generate(v);
    }

    public static void Configure(this ModelBuilder builder)
    {
        builder.Entity<Review>()
            .HasOne(x => x.Book)
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Rating>()
            .HasOne(x => x.Book)
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Book>()
            .HasMany(x => x.Ratings)
            .WithOne(x => x.Book)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(x => x.BookId);

        builder.Entity<Book>()
            .HasMany(x => x.Reviews)
            .WithOne(x => x.Book)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(x => x.BookId);
    }
}
