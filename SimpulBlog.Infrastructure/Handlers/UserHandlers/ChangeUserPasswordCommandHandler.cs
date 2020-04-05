using MediatR;
using SimpulBlog.Core.Exceptions;
using SimpulBlog.Domain.Entities.Concrete;
using SimpulBlog.Domain.Repositories.Abstract;
using SimpulBlog.Infrastructure.Commands.UserCommands;
using SimpulBlog.Infrastructure.Services.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace SimpulBlog.Infrastructure.Handlers.UserHandlers
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IEncrypter encrypter;
        private readonly ICommitRepository<User> commitRepository;

        private User user;

        public ChangeUserPasswordCommandHandler(
            IUserRepository userRepository,
            IEncrypter encrypter,
            ICommitRepository<User> commitRepository
            )
        {
            this.userRepository = userRepository;
            this.encrypter = encrypter;
            this.commitRepository = commitRepository;
        }

        public async Task<Unit> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            await CheckIfUserExsist(request);
            CheckCurrentPassword(request.CurrentPassword);
            ChangePassword(request.NewPassword);
            await SaveUser();

            return Unit.Value;
        }

        private async Task CheckIfUserExsist(ChangeUserPasswordCommand request)
        {
            user = await userRepository.GetById(request.UserId);
            if (!user.IsActive)
                throw new BlogException(ErrorCode.NotFound);
        }

        private void CheckCurrentPassword(string oldPassword)
        {
            var hash = encrypter.GetHash(oldPassword, user.Salt);
            encrypter.CompareHash(hash, user.Hash);
        }

        private void ChangePassword(string newPassword)
        {
            var salt = encrypter.GetSalt(newPassword);
            var hash = encrypter.GetHash(newPassword, salt);

            user.ChangePassword(hash, salt);
        }

        private async Task SaveUser()
        {
            await commitRepository.Update(user);
            await commitRepository.Commit();
        }
    }
}
