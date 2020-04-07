using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpulBlog.Infrastructure.Dtos.ArticleDtos;
using SimpulBlog.Infrastructure.Handlers.ArticleHanlders;
using SimpulBlog.Infrastructure.Queries.ArticleQueries;

namespace SimpulBlog.API.Controllers
{
    [Route("api/article")]
    public class ArticleController : AbstractController
    {
        public ArticleController(IMediator mediatr) : base(mediatr)
        {
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
            => Ok(await Handle(new GetArticleQuery(id)));


    }
}