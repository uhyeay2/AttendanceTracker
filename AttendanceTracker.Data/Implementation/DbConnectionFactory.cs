namespace AttendanceTracker.Data.Implementation
{
    internal class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString) => _connectionString = connectionString;

        public System.Data.IDbConnection NewConnection() => new System.Data.SqlClient.SqlConnection(_connectionString);
    }
}
