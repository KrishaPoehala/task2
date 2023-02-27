using task2.DAL.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using task2.WebApi.Middlewares;
using FluentValidation;
using task2.Infrastructure.Services.Abstration;
using task2.Infrastructure.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Add services to the container.
builder.Services.AddMediatR(x =>
{
    x.RegisterServicesFromAssembly(typeof(task2.BLL.AssemblyReference).Assembly);
});
builder.Services.AddControllers().AddJsonOptions(option =>
option.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<BooksContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BooksDb")));
builder.Services.AddAutoMapper(typeof(task2.Infrastructure.Profiles.MappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<task2.BLL.AssemblyReference>();
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

InitializeDb();
app.Run();
void InitializeDb()
{
    using var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<BooksContext>();
    context.Database.Migrate();
}