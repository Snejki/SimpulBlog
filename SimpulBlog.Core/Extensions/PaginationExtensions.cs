namespace SimpulBlog.Core.Extensions
{
    public static class PaginationExtensions
    {
        public static int GetPagesCount(this int elementsCount, int pageSize)
        {
            return (elementsCount + pageSize - 1) / pageSize;
        }
    }
}
