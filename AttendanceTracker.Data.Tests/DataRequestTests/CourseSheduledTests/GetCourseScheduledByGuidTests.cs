using AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.CourseScheduledTests
{
    public class GetCourseScheduledByGuidTests : DataTest
    {
        [Fact]
        public async Task GetCourseScheduledByGuid_Given_CourseScheduledNotExisting_ShouldReturn_Null()
        {
            var result = await _dataAccess.FetchAsync(new GetCourseScheduledByGuid(Guid.NewGuid()));

            Assert.Null(result);
        }

        [Fact]
        public async Task GetCourseScheduledByGuid_Given_CourseIsExisting_Should_ReturnCourse()
        {
            var expectedGuid = Guid.NewGuid();
            var expectedCourse = await SeedAsync(new SeedCourseRequest());
            var expectedInstructor = await SeedAsync(new SeedInstructorRequest());
            var expectedStartDate = new DateTime(2023, 5, 22);
            var expectedEndDate = new DateTime(2023, 7, 20);

            var expected = await SeedAsync(new SeedCourseScheduledRequest(expectedGuid, expectedCourse.CourseCode, expectedInstructor.InstructorCode, 
                                                                          expectedStartDate, expectedEndDate));

            var result = await _dataAccess.FetchAsync(new GetCourseScheduledByGuid(expectedGuid));

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expected.Id, result.Id);
                Assert.Equal(expectedGuid, result.Guid);
                Assert.Equal(expectedCourse.Id, result.CourseId);
                Assert.Equal(expectedInstructor.Id, result.InstructorId);
                Assert.Equal(expectedStartDate, result.StartDate);
                Assert.Equal(expectedEndDate, result.EndDate);
            });
        }
    }
}
