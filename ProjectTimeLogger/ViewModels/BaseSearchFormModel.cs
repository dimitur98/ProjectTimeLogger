using ProjectTimeLogger.Db.Models.Search;
using ProjectTimeLogger.ViewModels.Contracts;

namespace ProjectTimeLogger.ViewModels
{
    public abstract class BaseSearchFormModel : IBaseSearchFormModel
    {
        public int RowCount = 10;

        public int? Page { get; set; }

        public string SortBy { get; set; }
        public bool? SortDesc { get; set; }

        protected virtual void SetSearchRequest(BaseRequest request)
        {
            if (this.Page < 1) { this.Page = null; }

            request.RowCount = this.RowCount;
            request.Offset = ((this.Page ?? 1) - 1) * this.RowCount;

            if (!string.IsNullOrWhiteSpace(this.SortBy))
            {
                request.SortDesc = this.SortDesc == true;
                request.SortColumn = this.SortBy;
            }
        }
    }
}