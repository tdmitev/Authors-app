using AutoMapper;
using SportScore2.Api.Models;
using SportScore2.Api.DTO;
namespace SportScore2.Api.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Authors
        CreateMap<Author, AuthorDto>();
        CreateMap<CreateAuthorDto, Author>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
        CreateMap<UpdateAuthorDto, Author>();

        // Articles
        CreateMap<Article, ArticleDto>()
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src =>
                src.Content.Length > 100
                    ? src.Content.Substring(0, 100) + "..."
                    : src.Content));
        CreateMap<CreateArticleDto, Article>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.AuthorId, opt => opt.Ignore())
            .ForMember(dest => dest.Published, opt => opt.MapFrom(src => src.Published ?? DateTime.UtcNow));
        CreateMap<UpdateArticleDto, Article>();
    }
}