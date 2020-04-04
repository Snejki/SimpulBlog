using SimpulBlog.Domain.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpulBlog.Domain.Repositories.Abstract
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetById(long id);
        Task<User> GetByEmail(string email);
        Task<ICollection<User>> GetAll();

        Task MakeSureUserWithProvidedEmailNotExsist(string email);
    }
}
