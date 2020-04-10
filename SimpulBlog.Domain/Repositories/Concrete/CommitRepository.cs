using Microsoft.EntityFrameworkCore;
using SimpulBlog.Core.Exceptions;
using SimpulBlog.Domain.Entities.Abstract;
using SimpulBlog.Domain.Repositories.Abstract;
using System;
using System.Threading.Tasks;

namespace SimpulBlog.Domain.Repositories.Concrete
{
    public class CommitRepository<T> : ICommitRepository<T> where T : Entity
    {
        private readonly BlogContext context;
        private readonly DbSet<T> dbSet;

        public CommitRepository(BlogContext context)
        {
            this.context = context;
            dbSet = this.context.Set<T>();
        }

        public async Task Add(T entity)
            => await dbSet.AddAsync(entity);


        public async Task Remove(T entity)
        {
            if (!(entity is IDeleteAble able))
                await Task.FromResult(dbSet.Remove(entity));
        }

        public async Task Update(T entity)
            => await Task.FromResult(dbSet.Update(entity));

        public async Task Commit()
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new BlogException(ErrorCode.FaultWhileSavingToDatabase);
            }
        }
    }
}
