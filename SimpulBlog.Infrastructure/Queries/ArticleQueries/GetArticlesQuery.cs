using MediatR;
using SimpulBlog.Infrastructure.Dtos.ArticleDtos;

namespace SimpulBlog.Infrastructure.Queries.ArticleQueries
{
    public class GetArticlesQuery : IRequest<ArticlesWithPaginationDto>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long CategoryId { get; set; }

        public GetArticlesQuery()
        {
            
        }

        public GetArticlesQuery(int page, int pageSize, long categoryId)
        {
            Page = page > 0 ? page : 1;
            PageSize = pageSize > 0 ? pageSize: 10;
            CategoryId = categoryId;
        }
    }
}
