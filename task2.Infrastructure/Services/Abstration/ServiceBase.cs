using AutoMapper;
using task2.DAL.Context;

namespace task2.Infrastructure.Services.Abstration;

public class ServiceBase
{
    protected readonly IMapper _mapper;
    protected readonly BooksContext _context;

    public ServiceBase(IMapper mapper, BooksContext context)
    {
        _mapper = mapper;
        _context = context;
    }
}
