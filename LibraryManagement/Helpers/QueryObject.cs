namespace LibraryManagement.Helpers
{
    public class QueryObject
    {
        public string? Title { get; set; } = null;
        public string? Author { get; set; }= null;
        public string? SortBy { get; set; } = null;
        public string? Genre { get; set; } = null;
        public bool isDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
