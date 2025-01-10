namespace ProjectTimeLogger.ViewModels.Contracts
{
    public interface IBaseSearchFormModel
    {
        int? Page { get; set; }

        string SortBy { get; set; }

        bool? SortDesc { get; set; }
    }
}
