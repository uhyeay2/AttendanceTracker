using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Domain.Extensions;
using System.Data.SqlClient;

namespace AttendanceTracker.Data.Tests.DataRequestTests.CourseTests
{
    public class InsertCourseTests : DataTest
    {
        [Fact]
        public async Task InsertCourse_Given_CourseIsInserted_ShouldReturn_RowsUpdated()
        {
            var courseCode = RandomString();

            var subject = await GetSeededSubjectAsync();

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertCourse(courseCode, subject.SubjectCode, RandomString()));

            await _dataAccess.ExecuteAsync(new DeleteCourse(courseCode));

            Assert.True(rowsAffected.AnyRowsAreUpdated());
        }

        [Fact]
        public async Task InsertCourse_Given_CourseCodeAlreadyTaken_ShouldThrow_SqlException()
        {
            var subject = await GetSeededSubjectAsync();

            var courseAlreadyExisting = await GetSeededCourseAsync();

            var exception = await Record.ExceptionAsync(async () => await _dataAccess
                                        .ExecuteAsync(new InsertCourse(courseAlreadyExisting.CourseCode, subject.SubjectCode, RandomString())));
            Assert.Multiple(() =>
            {
                Assert.NotNull(exception);

                Assert.IsType<SqlException>(exception);
            });
        }

        [Fact]
        public async Task InsertCourse_Given_CourseCodeIsNull_ShouldThrow_SqlException()
        {
            var subject = await GetSeededSubjectAsync();

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(new InsertCourse(courseCode: null!, subject.SubjectCode, RandomString())));
        }
    }
}
