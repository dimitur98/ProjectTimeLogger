using Microsoft.AspNetCore.Mvc;
using ProjectTimeLogger.Db;
using ProjectTimeLogger.ViewModels.Api;

namespace ProjectTimeLogger.Controllers.Api
{
    [Route("/api/TimeLogs/[action]")]
    public class TimeLogsApiController : BaseApiController
    {
        [HttpGet]
        public IActionResult GetTotalHoursByUser(uint userId, DateTime? dateFrom, DateTime? dateTo)
        {
            var response = ProjectTimeLoggerDb.TimeLogs.GetTotalHoursByUser(userId, dateFrom, dateTo);

            return this.Ok(new TimeLogHoursModel { Name = response.User.FullName, Hours = response.TotalHours});
        }

        [HttpGet]
        public IActionResult GetTotalHoursByUsers(DateTime? dateFrom, DateTime? dateTo)
        {
            return this.Ok(ProjectTimeLoggerDb.TimeLogs.GetTotalHoursByUsers(dateFrom, dateTo, limit: 10).Select(r => new TimeLogHoursModel { Name = r.User.FullName, Hours = r.TotalHours}));
        }

        [HttpGet]
        public IActionResult GetTotalHoursByProjects(DateTime? dateFrom, DateTime? dateTo)
        {
            return this.Ok(ProjectTimeLoggerDb.TimeLogs.GetTotalHoursByProjects(dateFrom, dateTo).Select(r => new TimeLogHoursModel { Name = r.Project.Name, Hours = r.TotalHours }));
        }
    }
}
