using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpulBlog.Infrastructure.Commands.UserCommands;

namespace SimpulBlog.API.Controllers
{
    [Route("api/user")]
    [Authorize]
    public class UserController : AbstractController
    {
        public UserController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddUserCommand command)
            => Ok(await Handle(command));

        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            await Handle(command);
            return NoContent();
        }
    }
}