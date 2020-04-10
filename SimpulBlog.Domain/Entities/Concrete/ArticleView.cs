using SimpulBlog.Domain.Entities.Abstract;
using System;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class ArticleView : Entity
    {
        public DateTime AddedAt { get; private set; }

        public long ArticleId { get; private set; }
        public virtual Article Article { get; private set; }

        protected ArticleView()
        {
            
        }

        public ArticleView(long articleId, DateTime addedAt)
        {
            SetArticleId(articleId);
            SetAddedAt(addedAt);
        }

        private void SetArticleId(long articleId)
        {
            ArticleId = articleId;
        }

        private void SetAddedAt(DateTime addedAt)
        {
            AddedAt = addedAt;
        }
    }
}
