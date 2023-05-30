using AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;
using AttendanceTracker.Domain.Models;

namespace AttendanceTracker.Application.Tests.HandlerTests.CourseScheduledHandlerTests
{
    public class InsertCourseScheduledHandlerTests : HandlerTest
    {
        private readonly InsertCourseScheduledHandler _handler;

        public InsertCourseScheduledHandlerTests() => _handler = new(_mockDataAccess.Object, _mockOrchestrator.Object);

        [Fact]
        public async Task InsertCourseScheduled_Given_RowsAreUpdated_ShouldReturn_CourseInserted()
        {
            var expected = A.New<CourseScheduled>();

            SetupExecuteAsync<InsertCourseScheduled>(OneRowUpdated);
            SetupGetResponseAsync<GetCourseScheduledByGuidRequest, CourseScheduled>(expected);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task InsertCourseScheduled_Given_RowsNotUpdated_AndCourseCodeNotExisting_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<InsertCourseScheduled>(NoRowsUpdated);
            SetupFetchAsync<IsInstructorCodeExisting, bool>(true);

            SetupFetchAsync<IsCourseCodeExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task InsertCourseScheduled_Given_RowsNotUpdated_AndInstructorCodeNotExisting_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<InsertCourseScheduled>(NoRowsUpdated);
            SetupFetchAsync<IsCourseCodeExisting, bool>(true);

            SetupFetchAsync<IsInstructorCodeExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task InsertCourseScheduled_Given_RowsNotUpdated_AndInstructorAndCourseCodesExisting_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<InsertCourseScheduled>(NoRowsUpdated);
            SetupFetchAsync<IsCourseCodeExisting, bool>(true);
            SetupFetchAsync<IsInstructorCodeExisting, bool>(true);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
