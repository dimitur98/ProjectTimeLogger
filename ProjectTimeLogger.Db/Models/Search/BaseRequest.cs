namespace ProjectTimeLogger.Db.Models.Search
{
    public class BaseRequest
    {
        public string SortColumn { get; set; }
        public bool SortDesc { get; set; }

        public int? Offset { get; set; }

        public int? RowCount { get; set; }
    }
}
