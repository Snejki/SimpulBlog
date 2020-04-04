using Microsoft.EntityFrameworkCore;
using SimpulBlog.Core.Exceptions;
using SimpulBlog.Domain.Entities.Abstract;
using System.Linq;
using System.Threading.Tasks;

namespace SimpulBlog.Domain.Extensions
{
    public static class EntityExtensions
    {
        public static async Task<T> GetById<T>(this IQueryable<T> query, long id) where T : Entity
        {
            var entity = await query.SingleOrDefaultAsync(e => e.Id == id);

            if (entity == null
                || (entity as IDeleteAble).DeletedAt.HasValue)
                throw new BlogException(ErrorCode.NotFound, $"{typeof(T).Name} does not exsist");

            return entity;
        }
    }
}
