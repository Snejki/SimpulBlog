using System.Linq;
using SimpulBlog.Domain.Entities.Concrete;
using SimpulBlog.Infrastructure.Dtos.ArticleDtos;

namespace SimpulBlog.Infrastructure.Mappers
{
    public class AutoMapperConfiguration : AutoMapper.Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.Author,
                    opts => opts.MapFrom(src => src.User.Firstname + " " + src.User.Lastname))
                .ForMember(dest => dest.CommentsCount,
                    opts => opts.MapFrom(src => src.Comments.Count(x => !x.DeletedAt.HasValue)));
        }
    }
}
