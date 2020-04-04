using MediatR;
using SimpulBlog.Infrastructure.Dtos.UserDtos;

namespace SimpulBlog.Infrastructure.Queries.UserQueries
{
    public class AddUserQuery : IRequest<AddUserDto>, IAuthQuery
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public long UserId { get; set; }
    }
}
