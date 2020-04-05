using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpulBlog.Infrastructure.Commands.UserCommands;

namespace SimpulBlog.API.Controllers
{
    [Route("api/user")]
    public class UserController : AbstractController
    {
        public UserController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddUserCommand query)
            => Ok(await Handle(query));
    }
}