using System.Linq;
using SimpulBlog.Domain.Entities.Concrete;
using SimpulBlog.Infrastructure.Dtos.ArticleDtos;
using SimpulBlog.Infrastructure.Dtos.CommentDtos;
using SimpulBlog.Infrastructure.Dtos.TagDtos;

namespace SimpulBlog.Infrastructure.Mappers
{
    public class AutoMapperConfiguration : AutoMapper.Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Tag, TagDto>();
            CreateMap<Comment, CommentDto>();

            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.Author,
                    opts => opts.MapFrom(src => src.User.Firstname + " " + src.User.Lastname))
                .ForMember(dest => dest.CommentsCount,
                    opts => opts.MapFrom(src => src.Comments.Count(x => !x.DeletedAt.HasValue)));

            CreateMap<Article, ArticleDetailsDto>()
                .ForMember(dest => dest.Author,
                    opts => opts.MapFrom(src => src.User.Firstname + " " + src.User.Lastname))
                .ForMember(dest => dest.Tags, opts => opts.Ignore())
                .ForMember(dest => dest.Comments, opts => opts.Ignore());
        }
    }
}
