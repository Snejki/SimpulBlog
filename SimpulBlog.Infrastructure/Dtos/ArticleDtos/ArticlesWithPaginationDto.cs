using System.Collections.Generic;

namespace SimpulBlog.Infrastructure.Dtos.ArticleDtos
{
    public class ArticlesWithPaginationDto
    {
        public PaginationDto Pagination { get; set; }
        public ICollection<ArticleDto> Articles { get; set; }

        public ArticlesWithPaginationDto()
        {
            
        }

        public ArticlesWithPaginationDto(PaginationDto pagination, ICollection<ArticleDto> articles)
        {
            Pagination = pagination;
            Articles = articles;
        }
    }
}
