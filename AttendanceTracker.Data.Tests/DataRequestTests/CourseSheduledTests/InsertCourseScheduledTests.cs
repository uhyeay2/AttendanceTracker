using AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests;
using AttendanceTracker.Domain.Extensions;
using System.Data.SqlClient;

namespace AttendanceTracker.Data.Tests.DataRequestTests.CourseScheduledTests
{
    public class InsertCourseScheduledTests : DataTest
    {
        [Fact]
        public async Task InsertCourseScheduled_Given_CourseScheduledIsInserted_ShouldReturn_RowsUpdated()
        {
            var guid = Guid.NewGuid();

            var course = await SeedAsync(new SeedCourseRequest());
            var instructor = await SeedAsync(new SeedInstructorRequest());

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertCourseScheduled(guid, course.CourseCode, instructor.InstructorCode, DateTime.Now, DateTime.Now));

            await _dataAccess.ExecuteAsync(new DeleteCourseScheduled(guid));

            Assert.True(rowsAffected.AnyRowsAreUpdated());
        }

        [Fact]
        public async Task InsertCourseScheduled_Given_GuidAlreadyTaken_ShouldThrow_SqlException()
        {
            var guid = Guid.NewGuid();

            var courseScheduledAlreadyExisting = await SeedAsync(new SeedCourseScheduledRequest(guid));

            // Get different Course/Instructor
            var course = await SeedAsync(new SeedCourseRequest());
            var instructor = await SeedAsync(new SeedInstructorRequest());

            var exception = await Record.ExceptionAsync(async () => await _dataAccess.ExecuteAsync(
                                new InsertCourseScheduled(courseScheduledAlreadyExisting.Guid, course.CourseCode, instructor.InstructorCode, DateTime.Now, DateTime.Now)));
            Assert.Multiple(() =>
            {
                Assert.NotNull(exception);

                Assert.IsType<SqlException>(exception);
            });
        }

        [Fact]
        public async Task InsertCourseScheduled_Given_CourseCodeNotExisting_ShouldThrow_SqlException()
        {
            var instructor = await SeedAsync(new SeedInstructorRequest());

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(
                new InsertCourseScheduled(Guid.NewGuid(), courseCode: RandomString(), instructor.InstructorCode, DateTime.Now, DateTime.Now)));
        }

        [Fact]
        public async Task InsertCourseScheduled_Given_InstructorCodeNotExisting_ShouldThrow_SqlException()
        {
            var course = await SeedAsync(new SeedCourseRequest());

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(
                new InsertCourseScheduled(Guid.NewGuid(), courseCode: course.CourseCode, instructorCode: RandomString(), DateTime.Now, DateTime.Now)));
        }
    }
}
