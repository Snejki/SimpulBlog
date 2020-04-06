using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpulBlog.Infrastructure.Commands.UserArticleCommands;

namespace SimpulBlog.API.Controllers
{
    [Route("api/user/article")]
    [Authorize]
    public class UserArticleController : AbstractController
    {
        public UserArticleController(IMediator mediatr) : base(mediatr)
        {
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> AddArticle([FromForm] AddArticleCommand command)
        {
            var response = await Handle(command);
            return Ok();
        }
    }
}