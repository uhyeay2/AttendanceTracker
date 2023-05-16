namespace AttendanceTracker.Data.Abstraction.Interfaces
{
    internal interface IDbConnectionFactory
    {
        System.Data.IDbConnection NewConnection();
    }
}
