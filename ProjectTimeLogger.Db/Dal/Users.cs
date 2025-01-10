using ProjectTimeLogger.Db.Dal;
using ProjectTimeLogger.Db.Models;
using ProjectTimeLogger.Db.Models.Search.Users;

namespace ProjectTimeLogger.Db.DAL
{
    public class Users : BaseDbSet<User, uint>
    {
        public Users(ProjectTimeLoggerDb db) : base(db, "user", "u", "id")
        {
        }

        public Response Search(Request request)
        {
            var queryParams = new
            {
                dateFrom = request.DateFrom,
                dateTo = request.DateTo,
                offset = request.Offset,
                rowCount = request.RowCount
            };

            var response = new Response
            {
                Records = _db.Mapper.Query<User>(this.GetSqlQuery(request), queryParams).ToList(),
                TotalRecords = _db.Mapper.Query<int>(this.GetSqlQuery(request, returnTotalRecords: true), queryParams).FirstOrDefault()
            };

            return response;
        }

        private string GetSqlQuery(Request request, bool returnTotalRecords = false)
        {
            var sql = $@"SELECT {(returnTotalRecords ? "COUNT(*)" : "*")}
                FROM `user` u
                WHERE 1 = 1";

            if (request.DateFrom.HasValue) { sql += " AND EXISTS(SELECT * FROM time_log tl WHERE tl.user_id = u.id AND tl.date >= @dateFrom)"; }
            if (request.DateTo.HasValue) { sql += " AND EXISTS(SELECT * FROM time_log tl WHERE tl.user_id = u.id AND tl.date <= @dateTo)"; }

            if (string.IsNullOrEmpty(request.SortColumn))
            {
                sql += " ORDER BY u.first_name, u.last_name";
            }
            else
            {
                sql += $" ORDER BY {request.SortColumn} {(request.SortDesc ? "DESC" : "")}";
            }

            if (!returnTotalRecords) { sql += " LIMIT @offset, @rowCount;"; }

            return sql;
        }
    }
}