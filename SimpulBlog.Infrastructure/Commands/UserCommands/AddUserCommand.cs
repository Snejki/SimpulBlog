using MediatR;
using SimpulBlog.Infrastructure.Dtos.UserDtos;

namespace SimpulBlog.Infrastructure.Commands.UserCommands
{
    public class AddUserCommand : IRequest<AddUserDto>, IAuthCommand
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public long UserId { get; set; }
    }
}
