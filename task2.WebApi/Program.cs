using task2.DAL.Context;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using task2.BLL.Services.Abstration;
using task2.BLL.Services.Implementation;
using task2.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Logging.AddJsonConsole();
// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(option =>
option.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<BooksContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BooksDb")));
builder.Services.AddAutoMapper(typeof(task2.Common.Profiles.MappingProfile));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddSingleton<IConfiguration>(p => builder.Configuration);
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
app.UseCors("corsapp");

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<task2.WebApi.Middlewares.ErrorHandlerMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
