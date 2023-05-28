using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentCourseScheduledTests
{
    public class IsStudentCourseScheduledExistingTests : DataTest
    {
        [Fact]
        public async Task IsStudentCourseScheduledExisting_Given_StudentCourseScheduledNotExisting_ShouldReturn_False()
        {
            var result = await _dataAccess.FetchAsync(new IsStudentCourseScheduledExisting(RandomString(), Guid.NewGuid()));

            Assert.False(result);
        }

        [Fact]
        public async Task IsStudentCourseScheduledExisting_Given_StudentCourseScheduledIsExisting_ShouldReturn_True()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            var existingStudentCourseScheduled = await SeedAsync(new SeedStudentCourseScheduledRequest(existingStudent.StudentCode));

            var result = await _dataAccess.FetchAsync(new IsStudentCourseScheduledExisting(existingStudent.StudentCode, existingStudentCourseScheduled.Guid));

            Assert.True(result);
        }
    }
}
