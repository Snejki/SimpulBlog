using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpulBlog.Infrastructure.Queries.AuthQueries;

namespace SimpulBlog.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : AbstractController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginUserQuery query)
            => Ok(await Handle(query));

        [HttpGet]
        
        public ActionResult AuthTest()
            => Ok(new { ok = "ok" });
    }
}