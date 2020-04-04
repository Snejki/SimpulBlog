using Microsoft.EntityFrameworkCore;
using SimpulBlog.Core.Exceptions;
using SimpulBlog.Domain.Entities.Concrete;
using SimpulBlog.Domain.Extensions;
using SimpulBlog.Domain.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpulBlog.Domain.Repositories.Concrete
{
    class UserRepository : IUserRepository
    {
        private readonly BlogContext context;

        public UserRepository(BlogContext context)
        {
            this.context = context;
        }

        public async Task<User> GetById(long id)
            => await context.Users.GetById(id);
        
        public async Task<User> GetByEmail(string email)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && !u.DeletedAt.HasValue);
            if(user == null)
                throw new BlogException(ErrorCode.NotFound, $"{nameof(email)} does not exsist");

            return user;
        }

        public async Task<ICollection<User>> GetAll()
        {
            return await context.Users.Where(u => !u.DeletedAt.HasValue).ToListAsync();
        }

        public async Task MakeSureUserWithProvidedEmailNotExsist(string email)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && !u.DeletedAt.HasValue);
            if (user != null)
                throw new BlogException(ErrorCode.AlreadyExists, $"User with provided email already exsist");
        }
    }
}
