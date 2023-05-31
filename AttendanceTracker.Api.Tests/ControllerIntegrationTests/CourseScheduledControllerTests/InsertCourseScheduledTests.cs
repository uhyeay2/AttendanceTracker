using AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers;
using AttendanceTracker.Domain.Constants;

namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.CourseScheduledControllerTests
{
    public class InsertCourseScheduledTests : BaseCourseScheduledControllerTest
    {
        public static readonly IEnumerable<object[]> InvalidRequests = new[]
        {
            // No input
            new[] { new InsertCourseScheduledRequest() },
            // No EndDate
            new[] { new InsertCourseScheduledRequest() { CourseCode = "C-CODE", InstructorCode = "I-CODE", StartDate = DateTime.Now  } },
            // No StartDate
            new[] { new InsertCourseScheduledRequest() { CourseCode = "C-CODE", InstructorCode = "I-CODE", EndDate= DateTime.Now  } },
            // No InstructorCode
            new[] { new InsertCourseScheduledRequest() { CourseCode = "C-CODE", StartDate = DateTime.Now, EndDate= DateTime.Now  } },
            // No CourseCode
            new[] { new InsertCourseScheduledRequest() { InstructorCode = "I-CODE", StartDate = DateTime.Now, EndDate= DateTime.Now  } },
            // Course Code Is Too Long
            new[] { new InsertCourseScheduledRequest() { CourseCode = "COURSE CODE IS TOO LONG", InstructorCode = "I-CODE", StartDate = DateTime.Now, EndDate= DateTime.Now  } },
            // Instructor Code Is Too Long
            new[] { new InsertCourseScheduledRequest() { CourseCode = "C-CODE", InstructorCode = "INSTRUCTOR CODE IS TOO LONG", StartDate = DateTime.Now, EndDate= DateTime.Now  } },
        };

        [Theory]
        [MemberData(nameof(InvalidRequests))]
        public async Task InsertCourseScheduled_Given_InvalidRequest_ShouldThrow_ValidationFailedException(InsertCourseScheduledRequest request)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.InsertCourseScheduled(request));
        }

        [Fact]
        public async Task InsertCourseScheduled_Given_InstructorNotExisting_ShouldThrow_DoesNotExistException()
        {
            var existingCourse = await SeedAsync(new SeedCourseRequest());

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.InsertCourseScheduled(
                new InsertCourseScheduledRequest(existingCourse.CourseCode, instructorCode: RandomString(InstructorCodeConstants.MaxLength), DateTime.Now, DateTime.Now)));
        }

        [Fact]
        public async Task InsertCourseScheduled_Given_CourseNotExisting_ShouldThrow_DoesNotExistException()
        {
            var existingInstructor = await SeedAsync(new SeedInstructorRequest());

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.InsertCourseScheduled(
                new InsertCourseScheduledRequest(courseCode: RandomString(CourseCodeConstants.MaxLength), existingInstructor.InstructorCode, DateTime.Now, DateTime.Now)));
        }

        [Fact]
        public async Task InsertCourseScheduled_Given_ValidRequest_ShouldReturn_ScheduledCourseInserted()
        {
            var existingCourse = await SeedAsync(new SeedCourseRequest());
            var existingInstructor = await SeedAsync(new SeedInstructorRequest());

            var result = await _controller.InsertCourseScheduled(new(existingCourse.CourseCode, existingInstructor.InstructorCode, DateTime.Now, DateTime.Now));

            await _controller.DeleteCourseScheduled(result.Guid);

            Assert.NotNull(result);
        }
    }
}
