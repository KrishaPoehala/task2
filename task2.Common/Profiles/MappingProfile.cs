using task2.Infrastructure.Dtos;
using task2.DAL.Entities;
using AutoMapper;
using task2.BLL.Commands;

namespace task2.Infrastructure.Profiles;

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
        CreateMap<SaveBookCommand, NewBookDto>();
        CreateMap<SaveReviewCommand, NewReviewDto>();
        CreateMap<RateABookCommand, NewScoreDto>();
        CreateMap<NewScoreDto, Rating>();
        CreateMap<NewReviewDto, Review>();
    }
}
