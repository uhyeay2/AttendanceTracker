namespace AttendanceTracker.Data.DataRequestObjects.StudentRequests
{
    public class GetAllStudentsPaginated : Parameterless_DataRequest<Student_DTO>
    {
        public GetAllStudentsPaginated(int pageNumber, int recordsPerPage)
        {
            PageNumber = pageNumber;
            RecordsPerPage = recordsPerPage;
        }

        public int PageNumber { get; set; }

        public int RecordsPerPage { get; set; }

        public override string GetSql() => Select.PaginatedFromTable(TableNames.Student, PageNumber, RecordsPerPage);
    }
}
