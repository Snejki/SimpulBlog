namespace SimpulBlog.Core.Helpers
{
    public static class CacheHelpers
    {
        public static string GetArticleCacheKey(long articleId) => $"Article_{articleId}";
    }
}
