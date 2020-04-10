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

        [HttpPut("{id:long}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> EditArticle(long id, [FromForm] EditArticleCommand command)
        {
            command.SetArticleId(id);
            await Handle(command);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult> DeleteArticle(long id)
        {
            await Handle(new DeleteArticleCommand(id));
            return NoContent();
        }
    }
}