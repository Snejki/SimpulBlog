using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimpulBlog.Domain.Entities.Concrete;
using SimpulBlog.Domain.Repositories.Abstract;
using SimpulBlog.Infrastructure.Dtos;
using SimpulBlog.Infrastructure.Dtos.ArticleDtos;
using SimpulBlog.Infrastructure.Queries.ArticleQueries;

namespace SimpulBlog.Infrastructure.Handlers.ArticleHanlders
{
    public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, ArticlesWithPaginationDto>
    {
        private readonly IArticleRepository articleRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        private ICollection<Article> articles;
        private PaginationDto pagination;

        public GetArticlesQueryHandler(IArticleRepository articleRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            this.articleRepository = articleRepository;
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
        }

        public async Task<ArticlesWithPaginationDto> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            await CheckIfCategoryExist(request.CategoryId);
            await GetPagination(request);
            await GetArticlesPage(request.CategoryId);

            return GetDto();
        }

        private async Task CheckIfCategoryExist(long categoryId)
        {
            if (categoryId != 0)
                await categoryRepository.GetById(categoryId);
        }

        private async Task GetPagination(GetArticlesQuery request)
        {
            var pagesCount = await articleRepository.GetPublishedPagesCount(request.PageSize, request.CategoryId);
            var currentPage = pagesCount < request.Page ? pagesCount : request.Page;

            pagination = new PaginationDto(pagesCount, currentPage, request.PageSize);
        }

        private async Task GetArticlesPage(long categoryId)
        {
            articles = await articleRepository.GetPublishedPage(pagination.CurrentPage, pagination.PageSize, categoryId);
        }

        private ArticlesWithPaginationDto GetDto()
        {
            return new ArticlesWithPaginationDto(
                pagination,
                mapper.Map<ICollection<ArticleDto>>(articles));
        }
    }
}
