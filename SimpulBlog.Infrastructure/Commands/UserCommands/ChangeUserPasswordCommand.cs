using MediatR;

namespace SimpulBlog.Infrastructure.Commands.UserCommands
{
    public class ChangeUserPasswordCommand : IRequest, IAuthCommand
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public long UserId { get; set; }
    }
}
