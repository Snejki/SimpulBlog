using SimpulBlog.Domain.Entities.Abstract;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class ArticleTag : Entity
    {
        public long ArticleId { get; set; }
        public virtual Article Article { get; set; }

        public long TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
