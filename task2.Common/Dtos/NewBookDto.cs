namespace task2.Common.Dtos;

public record NewBookDto(int? Id,string Title,string Cover,
    string Content,string Genre,string Author);
