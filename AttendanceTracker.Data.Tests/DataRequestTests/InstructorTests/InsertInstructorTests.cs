using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;
using AttendanceTracker.Domain.Constants;
using System.Data.SqlClient;

namespace AttendanceTracker.Data.Tests.DataRequestTests.InstructorTests
{
    public class InsertInstructorTests : DataTest
    {
        [Fact]
        public async Task InsertInstructor_Given_InstructorCode_IsNull_ShouldThrow_SqlException()
        {
            var insertRequest = new InsertInstructor(instructorCode: null!, RandomString(), RandomString());

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(insertRequest));
        }

        [Fact]
        public async Task InsertInstructor_Given_InstructorCode_AlreadyExists_ShouldThrow_SqlException()
        {
            var existingInstructor = await SeedAsync(new SeedInstructorRequest());

            var insertRequestWithExistingInstructorCode = new InsertInstructor(existingInstructor.InstructorCode, RandomString(), RandomString());

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(insertRequestWithExistingInstructorCode));
        }

        [Fact]
        public async Task InsertInstructor_Given_InstructorIsInserted_ShouldReturn_One()
        {
            var instructorCode = Guid.NewGuid().ToString()[..InstructorCodeConstants.MaxLength];

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertInstructor(instructorCode, RandomString(), RandomString()));

            await _dataAccess.ExecuteAsync(new DeleteInstructor(instructorCode));

            Assert.Equal(1, rowsAffected);
        }
    }
}
