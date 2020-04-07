using MediatR;
using SimpulBlog.Infrastructure.Dtos.ArticleDtos;

namespace SimpulBlog.Infrastructure.Queries.ArticleQueries
{
    public class GetArticleQuery : IRequest<ArticleDetailsDto>
    {
        public long ArticleId { get; set; }

        public GetArticleQuery()
        {
            
        }

        public GetArticleQuery(long articleId)
        {
            ArticleId = articleId;
        }
    }
}
