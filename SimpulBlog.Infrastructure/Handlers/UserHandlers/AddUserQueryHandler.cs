using MediatR;
using SimpulBlog.Core.Enums;
using SimpulBlog.Core.Exceptions;
using SimpulBlog.Domain.Entities.Concrete;
using SimpulBlog.Domain.Repositories.Abstract;
using SimpulBlog.Infrastructure.Dtos.UserDtos;
using SimpulBlog.Infrastructure.Queries.UserQueries;
using System;
using System.Threading;
using System.Threading.Tasks;
using SimpulBlog.Core.Helpers;
using SimpulBlog.Infrastructure.Services.Abstract;

namespace SimpulBlog.Infrastructure.Handlers.UserHandlers
{
    public class AddUserQueryHandler : IRequestHandler<AddUserQuery, AddUserDto>
    {
        private readonly IUserRepository userRepository;
        private readonly ICommitRepository<User> commitRepository;
        private readonly IEncrypter encrypter;

        private User user = null;
        private string userPassword;

        public AddUserQueryHandler(
            IUserRepository userRepository,
            ICommitRepository<User> commitRepository,
            IEncrypter encrypter
            )
        {
            this.userRepository = userRepository;
            this.commitRepository = commitRepository;
            this.encrypter = encrypter;
        }

        public async Task<AddUserDto> Handle(AddUserQuery request, CancellationToken cancellationToken)
        {
            await CheckIfUserIsAdminAndCanAddUser(request.UserId);
            await MakeSureUserWithProvidedDataNotExsist(request.Email);
            PrepareUser(request);
            await AddUser();

            return new AddUserDto(userPassword);
        }

        private async Task CheckIfUserIsAdminAndCanAddUser(long userId)
        {
            var user = await userRepository.GetById(userId);
            if (user.UserRole != UserRole.Administrator)
                throw new BlogException(ErrorCode.NoPermission);
        }

        private async Task MakeSureUserWithProvidedDataNotExsist(string email)
        {
            await userRepository.MakeSureUserWithProvidedEmailNotExsist(email);
        }

        private void PrepareUser(AddUserQuery request)
        {
            userPassword = PasswordHelpers.GeneratePassword();
            var salt = encrypter.GetSalt(userPassword);
            var hash = encrypter.GetHash(userPassword, salt);
            DateTime addedAt = DateTimeHelpers.GetCurrenTime();

            user = new User(request.Email, request.Firstname, request.Lastname, hash, salt, UserRole.Writer, addedAt);
        }

        private async Task AddUser()
        {
            await commitRepository.Add(user);
            await commitRepository.Commit();
        }
    }
}
