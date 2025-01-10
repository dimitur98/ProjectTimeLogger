using Mysqlx.Crud;
using ProjectTimeLogger.Db.Dal;
using ProjectTimeLogger.Db.Models;

namespace ProjectTimeLogger.Db.DAL
{
    public class TimeLogs : BaseDbSet
    {
        public TimeLogs(ProjectTimeLoggerDb db) : base(db)
        {
        }

        public (uint UserId, User User, float TotalHours) GetTotalHoursByUser(uint userId, DateTime? dateFrom, DateTime? dateTo) 
            => GetTotalHoursByUsers(dateFrom, dateTo, userIds: [userId]).FirstOrDefault();

        public List<(uint UserId, User User, float TotalHours)> GetTotalHoursByUsers(DateTime? dateFrom, DateTime? dateTo, IEnumerable<uint> userIds = null, uint? limit = null) 
        {
            var totalHours = GetTotalHoursGroupedBy("user_id", dateFrom, dateTo, userIds: userIds, limit: limit);
            var users = ProjectTimeLoggerDb.Users.GetByIds(totalHours.Select(th => th.Id));
            
            return totalHours.Select(th => (th.Id, users.FirstOrDefault(u => u.Id == th.Id), th.TotalHours)).ToList();
        }

        public List<(uint ProjectId, Project Project, float TotalHours)> GetTotalHoursByProjects(DateTime? dateFrom, DateTime? dateTo) {
            var totalHours = GetTotalHoursGroupedBy("project_id", dateFrom, dateTo);
            var projects = ProjectTimeLoggerDb.Projects.GetByIds(totalHours.Select(th => th.Id));

            return totalHours.Select(th => (th.Id, projects.FirstOrDefault(p => p.Id == th.Id), th.TotalHours)).ToList();
        }

        private List<(uint Id, float TotalHours)> GetTotalHoursGroupedBy(string groupBy, DateTime? dateFrom, DateTime? dateTo, IEnumerable<uint> userIds = null, uint ? limit = null)
        {
            var sql = @$"SELECT {groupBy} AS id, SUM(hours) AS total_hours
                FROM time_log
                WHERE 1=1";

            if (dateFrom.HasValue) { sql += " AND date >= @dateFrom"; }
            if (dateTo.HasValue) { sql += " AND date <= @dateTo"; }
            if (userIds?.Any() == true) { sql += " AND user_id IN @userIds"; }

            sql += @$" GROUP BY {groupBy}
                ORDER BY total_hours DESC";

            if (limit.HasValue) { sql += " LIMIT @limit"; }

            var queryParams = new
            {
                dateFrom,
                dateTo,
                limit,
                userIds
            };

            return this._db.Mapper.Query<(uint Id, float TotalHours)>(sql, queryParams).ToList();
        }
    }
}