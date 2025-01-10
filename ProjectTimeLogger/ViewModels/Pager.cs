namespace ProjectTimeLogger.ViewModels
{
    public class Pager
    {
        private int MaxDisplayedPages = 10;

        public int Page { get; set; }

        public int ItemsPerPage { get; set; } = 10;

        public long TotalItemsCount { get; set; }

        public int PagesCount
        {
            get
            {
                var result = (int)Math.Ceiling(this.TotalItemsCount / (decimal)this.ItemsPerPage);

                return (result < 1) ? 1 : result;
            }
        }

        public List<int> DisplayedPages
        {
            get
            {
                var result = new List<int>();
                var maxPage = Math.Floor(TotalItemsCount / (double)ItemsPerPage);
                var start = Math.Max(this.Page - (int)Math.Floor(this.MaxDisplayedPages / (decimal)2), 1);
                var end = Math.Min((start - 1) + this.MaxDisplayedPages, this.PagesCount);

                for (var i = start; i <= end; i++)
                {
                    result.Add(i);
                }

                return result;
            }
        }

        public Pager(int page, int itemsPerPage, long totalItemsCount)
        {
            this.Page = page;
            this.ItemsPerPage = itemsPerPage;
            this.TotalItemsCount = totalItemsCount;
        }
    }
}