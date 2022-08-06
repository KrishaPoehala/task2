using Microsoft.EntityFrameworkCore;
namespace task2.DAL.Context;
using Extentions;
using task2.DAL.Entities;

public class BooksContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public BooksContext(DbContextOptions<BooksContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Configure();
        modelBuilder.Seed();
    }
}
