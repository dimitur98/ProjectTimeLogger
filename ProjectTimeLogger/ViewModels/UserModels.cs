using ProjectTimeLogger.Db.Models;
using ProjectTimeLogger.Db.Models.Search.Users;
using System.ComponentModel;

namespace ProjectTimeLogger.ViewModels
{
    public class UsersSearchModel : BaseSearchModel<Response, UsersSearchFormModel, User>
    {
        public Dictionary<uint, float> HoursByUserDict { get; set; }

        public UsersSearchModel(UsersSearchFormModel searchForm) : base(searchForm) { }
    }

    public class UsersSearchFormModel : BaseSearchFormModel
    {
        [DisplayName("Date From")]
        public DateTime? DateFrom { get; set; }

        [DisplayName("Date To")]
        public DateTime? DateTo { get; set; }

        public Request ToSearchRequest()
        {
            var request = new Request
            {
                DateFrom = this.DateFrom,
                DateTo = this.DateTo
            };

            this.SetSearchRequest(request);

            return request;
        }
    }
}