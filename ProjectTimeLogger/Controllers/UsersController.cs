using Microsoft.AspNetCore.Mvc;
using ProjectTimeLogger.Db;
using ProjectTimeLogger.Models;
using ProjectTimeLogger.ViewModels;
using System.Diagnostics;

namespace ProjectTimeLogger.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Search(UsersSearchFormModel searchForm)
        {
            var model = new UsersSearchModel(searchForm)
            {
                Response = ProjectTimeLoggerDb.Users.Search(searchForm.ToSearchRequest())
            };

            if (model.Response.Records.Any())
            {
               model.HoursByUserDict = 
                    ProjectTimeLoggerDb.TimeLogs.GetTotalHoursByUsers(searchForm.DateFrom, searchForm.DateTo, userIds: model.Response.Records.Select(r => r.Id))
                    .ToDictionary(x => x.UserId, x => x.TotalHours);
            }

            return this.View(model);
        }

        public IActionResult ResetDb()
        {
            ProjectTimeLoggerDb.Initialize();

            return this.RedirectToAction(nameof(Search));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
