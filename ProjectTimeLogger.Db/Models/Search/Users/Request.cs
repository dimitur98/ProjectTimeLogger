namespace ProjectTimeLogger.Db.Models.Search.Users
{
    public class Request : BaseRequest
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
