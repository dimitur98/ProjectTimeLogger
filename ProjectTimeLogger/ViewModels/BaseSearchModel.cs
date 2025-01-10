using ProjectTimeLogger.Db.Models.Search;

namespace ProjectTimeLogger.ViewModels
{
    public abstract class BaseSearchModel<TSearchResponse, TSearchForm, T>
        where TSearchResponse : BaseResponse<T>
        where TSearchForm : BaseSearchFormModel
    {
        public TSearchResponse Response { get; set; }

        public TSearchForm SearchForm { get; set; }

        protected BaseSearchModel(TSearchForm searchForm)
        {
            this.SearchForm = searchForm;
        }

        public virtual Pager ToPager()
        {
            return new Pager(this.SearchForm.Page ?? 1, this.SearchForm.RowCount, this.Response.TotalRecords);
        }
    }
}