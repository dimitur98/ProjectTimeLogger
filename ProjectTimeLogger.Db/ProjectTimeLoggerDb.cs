using ProjectTimeLogger.Db.DAL;

namespace ProjectTimeLogger.Db
{
    public class ProjectTimeLoggerDb : BaseDb
    {
        private static ProjectTimeLoggerDb _instance = null;

        private Projects _projects;
        private TimeLogs _timeLogs;
        private Users _users;

        public static Projects Projects => _instance._projects ??= new Projects(_instance);
        public static TimeLogs TimeLogs => _instance._timeLogs ??= new TimeLogs(_instance);
        public static Users Users => _instance._users ??= new Users(_instance);

        private ProjectTimeLoggerDb(string connectionString, int? unableToConnectToHostErrorRetryInterval = null, bool unableToConnectToHostErrorRetryTillConnect = false)
            : base(connectionString, unableToConnectToHostErrorRetryInterval: unableToConnectToHostErrorRetryInterval, unableToConnectToHostErrorRetryTillConnect: unableToConnectToHostErrorRetryTillConnect)
        {
        }

        public static void SetupConnection(string connectionString, int? unableToConnectToHostErrorRetryInterval = null, bool unableToConnectToHostErrorRetryTillConnect = false)
        {
            _instance = new ProjectTimeLoggerDb(connectionString, unableToConnectToHostErrorRetryInterval: unableToConnectToHostErrorRetryInterval, unableToConnectToHostErrorRetryTillConnect: unableToConnectToHostErrorRetryTillConnect);
        }

        public static void Initialize()
        {
            _instance.Mapper.Execute("sp_initialize_db");
        }
    }
}