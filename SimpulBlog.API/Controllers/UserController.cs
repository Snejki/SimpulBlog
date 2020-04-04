using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpulBlog.Infrastructure.Queries.UserQueries;

namespace SimpulBlog.API.Controllers
{
    [Route("api/user")]
    public class UserController : AbstractController
    {
        public UserController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddUserQuery query)
            => Ok(await Handle(query));
    }
}