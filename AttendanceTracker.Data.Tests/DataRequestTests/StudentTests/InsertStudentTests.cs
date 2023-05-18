using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Domain.Constants;
using System.Data.SqlClient;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class InsertStudentTests : DataTest
    {
        [Fact]
        public async Task InsertStudent_Given_StudentCode_IsNull_ShouldThrow_SqlException()
        {
            var insertRequest = new InsertStudent(studentCode: null!, "FirstName", "LastName", DateTime.Now);

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(insertRequest));
        }

        [Fact]
        public async Task InsertStudent_Given_StudentCode_AlreadyExists_ShouldThrow_SqlException()
        {
            var existingStudent = await _dataSeeder.NewStudent();

            var insertRequestWithExistingStudentCode = new InsertStudent(existingStudent.StudentCode, "NewFirstName", "OtherLastName", DateTime.Now);

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(insertRequestWithExistingStudentCode));
        }

        [Fact]
        public async Task InsertStudent_Given_StudentIsInserted_ShouldReturn_One()
        {
            var studentCode = Guid.NewGuid().ToString()[..StudentCodeConstants.ExpectedLength];

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertStudent(studentCode, "FirstName", "LastName", DateTime.Now));

            await _dataAccess.ExecuteAsync(new DeleteStudent(studentCode));

            Assert.Equal(1, rowsAffected);
        }
    }
}
