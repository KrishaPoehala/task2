using task2.DAL.Context;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using task2.BLL.Services.Abstration;
using task2.BLL.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(option =>
option.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<BooksContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BooksDb")));
builder.Services.AddAutoMapper(typeof(task2.Common.Profiles.MappingProfile));
builder.Services.AddScoped<IBookService, BookService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
