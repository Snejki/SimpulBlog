using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SimpulBlog.Infrastructure.Commands;
using SimpulBlog.Infrastructure.Dtos;
using SimpulBlog.Infrastructure.Queries;

namespace SimpulBlog.API.Controllers
{
    [Route("api")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AbstractController : ControllerBase
    {
        private readonly IMediator mediatr;
        //private long userId => GetLoggedUserId();
        private long userId => 2;

        protected AbstractController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }

        private long GetLoggedUserId()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                var userIdString = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                if (Int64.TryParse(userIdString, out long userId))
                {
                    return userId;
                }
            }

            return -1;
        }


        protected async Task<T> Handle<T>(IRequest<T> request)
        {
            if (request is AuthQuery)
            {
                (request as AuthQuery).UserId = userId;
            }

            if (request is AuthCommand)
            {
                (request as AuthCommand).UserId = userId;
            }

            return await mediatr.Send(request);
        }

        protected void AddPaginationHeaders(PaginationDto pagination)
        {
            Response.Headers.Add("Pagination-Current-Page", pagination.CurrentPage.ToString());
            Response.Headers.Add("Pagination-Page-Size", pagination.PageSize.ToString());
            Response.Headers.Add("Pagination-Pages-Count", pagination.PagesCount.ToString());
        }
    }
}