using System.Data.SqlClient;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class InsertStudentTests : DataTest
    {
        [Fact]
        public async Task InsertStudent_Given_StudentCode_IsNull_ShouldThrow_SqlException()
        {
            var insertRequest = _requestFactory.InsertStudent();

            insertRequest.Student.StudentCode = null!;

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(insertRequest));
        }

        [Fact]
        public async Task InsertStudent_Given_StudentCode_AlreadyExists_ShouldThrow_SqlException()
        {
            var insertRequest = _requestFactory.InsertStudent();

            await _dataAccess.ExecuteAsync(insertRequest);

            var insertRequestWithSameCode = _requestFactory.InsertStudent(insertRequest.Student.StudentCode);

            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(insertRequestWithSameCode));

            await _dataAccess.ExecuteAsync(_requestFactory.DeleteStudent());

            Assert.NotNull(exception);
            Assert.IsType<SqlException>(exception);
        }

        [Fact]
        public async Task InsertStudent_Given_StudentIsInserted_ShouldReturn_One()
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(_requestFactory.InsertStudent());

            await _dataAccess.ExecuteAsync(_requestFactory.DeleteStudent());

            Assert.Equal(1, rowsAffected);
        }
    }
}
