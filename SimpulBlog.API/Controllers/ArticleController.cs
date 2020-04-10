using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SimpulBlog.Core.Helpers;
using SimpulBlog.Infrastructure.Commands.ArticleViewmCommands;
using SimpulBlog.Infrastructure.Dtos.ArticleDtos;
using SimpulBlog.Infrastructure.Queries.ArticleQueries;

namespace SimpulBlog.API.Controllers
{
    [Route("api/article")]
    public class ArticleController : AbstractController
    {
        private readonly IMemoryCache memoryCache;
        public ArticleController(IMediator mediatr, IMemoryCache memoryCache) : base(mediatr)
        {
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ArticleDto>>> Get(int page = 1, int pageSize = 10, long categoryId = 0)
        {
            var response = await Handle(new GetArticlesQuery(page, pageSize, categoryId));
            AddPaginationHeaders(response.Pagination);
            return Ok(response.Articles);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ArticleDetailsDto>> Get(long id)
        {
            var article = await HandleWithCache(new GetArticleQuery(id), CacheHelpers.GetArticleCacheKey(id));
            await Handle(new AddArticleViewCommand(id));

            return Ok(article);
        }

        protected async Task<T> HandleWithCache<T>(IRequest<T> request, string cacheKey)
        {
            if (memoryCache.TryGetValue(cacheKey, out T response))
            {
                return response;
            }

            response = await Handle(request);
            memoryCache.Set(cacheKey, response, TimeSpan.FromHours(12));

            return response;
        }
    }
}