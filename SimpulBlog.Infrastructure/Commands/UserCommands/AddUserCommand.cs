using MediatR;
using SimpulBlog.Infrastructure.Dtos.UserDtos;

namespace SimpulBlog.Infrastructure.Commands.UserCommands
{
    public class AddUserCommand : AuthCommand, IRequest<AddUserDto>
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
