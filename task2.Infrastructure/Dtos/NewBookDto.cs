namespace task2.Infrastructure.Dtos;

public record NewBookDto(int? Id,string Title,string Cover,
    string Content,string Genre,string Author);