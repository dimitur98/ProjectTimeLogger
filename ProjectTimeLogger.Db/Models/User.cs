using ProjectTimeLogger.Db.DapperMySqlMapper;

namespace ProjectTimeLogger.Db.Models
{
    public class User : BaseModel<uint>
    {
        [Column(Name = "first_name")]
        public string FirstName { get; set; }

        [Column(Name = "last_name")]
        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        [Column(Name = "email")]
        public string Email { get; set; }
    }
}