using ProjectTimeLogger.Db.Models;

namespace ProjectTimeLogger.Db.Dal
{
    public abstract class BaseDbSet
    {
        protected readonly BaseDb _db;
        protected BaseDbSet(BaseDb db)
        {
            _db = db;
        }
    }

    public abstract class BaseDbSet<T, TId>
        where T : BaseModel<TId>
    {
        protected readonly BaseDb _db;
        protected readonly string _table;
        protected readonly string _tableAlias;
        protected readonly string _id;

        protected BaseDbSet(BaseDb db, string table, string tableAlias, string id)
        {
            _db = db;
            _table = table;
            _tableAlias = tableAlias;
            _id = id;
        }

        public virtual List<T> GetByIds(IEnumerable<TId> ids)
        {
            var sql = $@"SELECT *
                FROM {_table} {_tableAlias}
                WHERE {_tableAlias}.{_id} IN @ids";

            return _db.Mapper.Query<T>(sql, new { ids }).ToList();
        }
    }
}
