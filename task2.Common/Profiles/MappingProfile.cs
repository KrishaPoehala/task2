using task2.Common.Dtos;
using task2.DAL.Entities;
using AutoMapper;
namespace task2.Common.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(x => x.Rating,
            opt => opt.MapFrom(src => src.Ratings.Count != 0 ? 
                  (decimal)src.Ratings.Sum(x => x.Score) / src.Ratings.Count : 0))
            .ForMember(x => x.ReviewsNumber,
            opt => opt.MapFrom(src => src.Reviews.Count));

        CreateMap<Book, BookWithReviewsDto>()
           .ForMember(x => x.Rating,
           opt => opt.MapFrom(src => src.Ratings.Count != 0 ? 
           (decimal)src.Ratings.Sum(x => x.Score) / src.Ratings.Count : 0));

        CreateMap<NewBookDto, Book>();
        CreateMap<Review, ReviewDto>();
    }
}
