using Microsoft.EntityFrameworkCore;
using SimpulBlog.Domain.Entities.Concrete;

namespace SimpulBlog.Domain
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> opts) : base(opts)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<ArticleView> ArticleViews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<SocialMedia> SocialMedia { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserSocialMedia> UserSocialMedia { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
