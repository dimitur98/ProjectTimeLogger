using ProjectTimeLogger.Db.DapperMySqlMapper;

namespace ProjectTimeLogger.Db.Models
{
    public class Project : BaseModel<uint>
    {
        [Column(Name = "name")]
        public string Name { get; set; }
    }
}
