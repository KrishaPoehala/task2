using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace task2.BLL.Commands;

public class SaveReviewCommand:IRequest<int>
{
    public string Message { get; set; }
    public string Reviewer { get; set; }
    [FromRoute]
    public int BookId { get; set; }
}
