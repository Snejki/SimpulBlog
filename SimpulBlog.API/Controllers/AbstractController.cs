using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpulBlog.Infrastructure.Commands;
using SimpulBlog.Infrastructure.Queries;

namespace SimpulBlog.API.Controllers
{
    [Route("api")]
    public class AbstractController : Controller
    {
        private readonly IMediator mediatr;
        private readonly long userId = 12;

        protected AbstractController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }

        protected async Task<T> Handle<T>(IRequest<T> request)
        {
            if (request is IAuthQuery)
            {
                (request as IAuthQuery).UserId = userId;
            }

            if (request is IAuthCommand)
            {
                (request as IAuthCommand).UserId = userId;
            }

            return await mediatr.Send(request);
        }
    }
}