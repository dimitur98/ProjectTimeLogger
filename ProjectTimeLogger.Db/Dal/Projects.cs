using ProjectTimeLogger.Db.Dal;
using ProjectTimeLogger.Db.Models;

namespace ProjectTimeLogger.Db.DAL
{
    public class Projects : BaseDbSet<Project, uint>
    {
        public Projects(BaseDb db) : base(db, "project", "p", "id")
        {
        }
    }
}
