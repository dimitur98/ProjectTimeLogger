using ProjectTimeLogger.Db.DapperMySqlMapper;

namespace ProjectTimeLogger.Db.Models
{
    public class TimeLog
    {
        [Column(Name = "user_id")]
        public uint UserId { get; set; }
        public User User { get; set; }

        [Column(Name = "project_id")]
        public uint ProjectId { get; set; }
        public Project Project { get; set; }

        [Column(Name = "date")]
        public DateTime Date { get; set; }

        [Column(Name = "hours")]
        public float Hours { get; set; }
    }
}