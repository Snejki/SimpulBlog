namespace SimpulBlog.Infrastructure.Dtos
{
    public class PaginationDto
    {
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public PaginationDto()
        {

        }

        public PaginationDto(int pagesCount, int currentPage, int pageSize)
        {
            PagesCount = pagesCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
    }
}
