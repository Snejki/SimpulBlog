using MediatR;
using System.Text.Json.Serialization;

namespace SimpulBlog.Infrastructure.Commands.UserCommands
{
    public class ChangeUserPasswordCommand : AuthCommand, IRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
