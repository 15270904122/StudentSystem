namespace StudentSystem.Infrastructure.Result
{
    public class PageResult<T> where T : class
    {
        public PageResult()
        {
            CurrentPage = 1;
            PageSize = 10;
        }
        public PageResult(int pageIndex, int pageSize)
        {
            CurrentPage = pageIndex;
            PageSize = pageSize;
        }
        public PageResult(int pageIndex, int pageSize, int totalCount)
        {
            CurrentPage = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public T Data { get; set; }
    }
}
