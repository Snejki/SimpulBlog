using MediatR;
using SimpulBlog.Infrastructure.Dtos.AuthDtos;

namespace SimpulBlog.Infrastructure.Queries.AuthQueries
{
    public class LoginUserQuery : IRequest<LoginUserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
