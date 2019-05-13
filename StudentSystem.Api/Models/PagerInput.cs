namespace StudentSystem.Api.Models
{
    public class PagerInput
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10000;
    }
}
