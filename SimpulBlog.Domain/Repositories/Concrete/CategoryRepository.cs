using System.Threading.Tasks;
using SimpulBlog.Domain.Entities.Concrete;
using SimpulBlog.Domain.Extensions;
using SimpulBlog.Domain.Repositories.Abstract;

namespace SimpulBlog.Domain.Repositories.Concrete
{
    class CategoryRepository : ICategoryRepository
    {
        private readonly BlogContext context;

        public CategoryRepository(BlogContext context)
        {
            this.context = context;
        }

        public Task<Category> GetById(long id)
            => context.Categories.GetById(id);
    }
}
