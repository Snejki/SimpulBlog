using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SimpulBlog.Domain.Entities.Concrete;
using SimpulBlog.Domain.Repositories.Abstract;
using SimpulBlog.Infrastructure.Dtos.ArticleDtos;
using SimpulBlog.Infrastructure.Dtos.CommentDtos;
using SimpulBlog.Infrastructure.Dtos.TagDtos;
using SimpulBlog.Infrastructure.Queries.ArticleQueries;

namespace SimpulBlog.Infrastructure.Handlers.ArticleHanlders
{
    public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, ArticleDetailsDto>
    {
        private readonly IArticleRepository articleRepository;
        private readonly IMapper mapper;

        private Article article;

        public GetArticleQueryHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            this.articleRepository = articleRepository;
            this.mapper = mapper;
        }

        public async Task<ArticleDetailsDto> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            await GetArticle(request.ArticleId);
            return GetDto();
        }

        private async Task GetArticle(long articleId)
            => article = await articleRepository.GetById(articleId);

        private ArticleDetailsDto GetDto()
        {
            var dto = mapper.Map<ArticleDetailsDto>(article);
            dto.Tags = mapper.Map<ICollection<TagDto>>(article.Tags);
            dto.Comments = mapper.Map<ICollection<CommentDto>>(article.Comments.Where(c => !c.DeletedAt.HasValue));

            return dto;
        }
    }
}
