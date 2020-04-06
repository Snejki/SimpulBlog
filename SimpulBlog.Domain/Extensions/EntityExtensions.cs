using System.Collections.Generic;
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

            if (entity == null)
                throw new BlogException(ErrorCode.NotFound, $"{typeof(T).Name} does not exist");

            if(entity is IDeleteAble able && able.DeletedAt.HasValue)
                throw new BlogException(ErrorCode.NotFound, $"{typeof(T).Name} does not exist");

            return entity;
        }

        public static async Task<ICollection<T>> Paginate<T>(this IQueryable<T> query, int page, int pageSize) where T : Entity
        {
            return await query.Skip((page - 1) * pageSize).Take(page * pageSize).ToListAsync();
        }
    }
}
