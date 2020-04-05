using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpulBlog.Infrastructure.Commands.UserCommands;
using SimpulBlog.Infrastructure.Dtos.UserDtos;

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
        public async Task<ActionResult<AddUserDto>> Post([FromBody] AddUserCommand command)
            => Ok(await Handle(command));

        [HttpPut("change-password")]
        public async Task<ActionResult> Put(ChangeUserPasswordCommand command)
        {
            await Handle(command);
            return NoContent();
        }
    }
}