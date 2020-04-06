using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpulBlog.Core.Extensions;
using SimpulBlog.Core.Helpers;
using SimpulBlog.Domain.Entities.Concrete;
using SimpulBlog.Domain.Extensions;
using SimpulBlog.Domain.Repositories.Abstract;

namespace SimpulBlog.Domain.Repositories.Concrete
{
    class ArticleRepository : IArticleRepository
    {
        private readonly BlogContext context;
        public ArticleRepository(BlogContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<Article>> GetPublishedPage(int page, int pageSize, long categoryId)
        {
            return await GetPublishedQueryFilteredByCategoryId(categoryId).Paginate(page, pageSize);
        }

        public async Task<int> GetPublishedPagesCount(int pageSize, long categoryId)
        {
            var articlesCount = await GetPublishedQueryFilteredByCategoryId(categoryId).CountAsync();
            return articlesCount.GetPagesCount(pageSize);
        }


        private IQueryable<Article> GetPublishedQueryFilteredByCategoryId(long categoryId)
        {
            var now = DateTimeHelpers.GetCurrenTime();
            var articlesQuery = context.Articles.Where(a => !a.DeletedAt.HasValue && a.PublishAt <= now);
            return  GetByCategoryId(articlesQuery, categoryId);
        }

        private IQueryable<Article> GetByCategoryId(IQueryable<Article> query,  long categoryId)
        {
            if (categoryId != 0)
            {
                query = query
                    .Join(
                        context.ArticleCategories,
                        article => article.Id,
                        articleCategory => articleCategory.ArticleId,
                        (article, articleCategory) => new { Article = article, ArticleCategory = articleCategory })
                    .Where(p => p.ArticleCategory.CategoryId == categoryId)
                    .Select(p => p.Article);
            }

            return query;
        }
    }
}
