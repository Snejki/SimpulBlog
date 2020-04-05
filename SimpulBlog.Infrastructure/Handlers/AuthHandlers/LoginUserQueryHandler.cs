using MediatR;
using SimpulBlog.Core.Exceptions;
using SimpulBlog.Domain.Entities.Concrete;
using SimpulBlog.Domain.Repositories.Abstract;
using SimpulBlog.Infrastructure.Dtos.AuthDtos;
using SimpulBlog.Infrastructure.Queries.AuthQueries;
using SimpulBlog.Infrastructure.Services.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace SimpulBlog.Infrastructure.Handlers.AuthHandlers
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginUserDto>
    {
        private readonly IUserRepository userRepository;
        private readonly IEncrypter encrypter;
        private readonly IJwtService jwtService;

        private User user;

        public LoginUserQueryHandler(
            IUserRepository userRepository,
            IEncrypter encrypter,
            IJwtService jwtService
            )
        {
            this.userRepository = userRepository;
            this.encrypter = encrypter;
            this.jwtService = jwtService;
        }

        public async Task<LoginUserDto> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            await CheckIfUserExsist(request);
            return new LoginUserDto(GenerateToken());
        }

        private async Task CheckIfUserExsist(LoginUserQuery request)
        {
            user = await userRepository.GetByEmail(request.Email);
            if (!user.IsActive)
                throw new BlogException(ErrorCode.NotFound);

            var hash = encrypter.GetHash(request.Password, user.Salt);
            encrypter.CompareHash(hash, user.Hash);
        }

        private string GenerateToken()
        {
            var token = jwtService.CreateToken(user.Id);
            return token;
        }
    }
}
