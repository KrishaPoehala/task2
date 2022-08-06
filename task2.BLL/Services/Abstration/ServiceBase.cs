using AutoMapper;
using task2.DAL.Context;

namespace task2.BLL.Services.Abstration;

public class ServiceBase
{
    protected readonly IMapper _mapper;
    protected readonly DAL.Context.BooksContext _context;

    public ServiceBase(IMapper mapper, BooksContext context)
    {
        this._mapper = mapper;
        _context = context;
    }
}
