namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class DeleteStudentTests : DataTest
    {
        [Fact]
        public async Task DeleteStudent_Given_StudentDoesNotExist_ShouldReturn_ZeroRowsAffected()
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(_requestFactory.DeleteStudent());

            Assert.Equal(0, rowsAffected);
        }

        [Fact]
        public async Task DeleteStudent_Given_StudentIsDeleted_ShouldReturn_OneRowAffected()
        {
            var insertRequest = _requestFactory.InsertStudent();
            var deleteRequest = _requestFactory.DeleteStudent(insertRequest.Student.StudentCode);

            await _dataAccess.ExecuteAsync(insertRequest);

            var rowsAffected = await _dataAccess.ExecuteAsync(deleteRequest);

            Assert.Equal(1, rowsAffected);
        }
    }
}
