namespace AttendanceTracker.Data.DataRequestObjects.LogRequests
{
    public class DeleteResponseTimeLog : Id_DataRequest
    {
        public DeleteResponseTimeLog(int id) : base(id) { }

        public override string GetSql() => Delete.FromTable(TableNames.ResponseTimeLog, where: "Id = @Id");
    }
}
