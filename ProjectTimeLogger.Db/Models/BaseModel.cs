using ProjectTimeLogger.Db.DapperMySqlMapper;

namespace ProjectTimeLogger.Db.Models
{
    public class BaseModel<T>
    {
        [Column(Name = "id")]
        public T Id { get; set; }
    }
}
