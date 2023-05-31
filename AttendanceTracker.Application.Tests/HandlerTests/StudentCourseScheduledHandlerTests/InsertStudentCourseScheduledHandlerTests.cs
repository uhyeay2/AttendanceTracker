using AttendanceTracker.Application.RequestHandlers.StudentCourseScheduledHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Domain.Models;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentCourseScheduledHandlerTests
{
    public class InsertStudentCourseScheduledHandlerTests : HandlerTest
    {
        private readonly InsertStudentCourseScheduledHandler _handler;

        public InsertStudentCourseScheduledHandlerTests() => _handler = new(_mockDataAccess.Object, _mockOrchestrator.Object);

        [Fact]
        public async Task InsertStudentCourseScheduled_Given_RowsUpdated_ShouldReturn_StudentCourseScheduled_FetchedFromOrchestrator()
        {
            var expected = A.New<StudentCourseScheduled>();

            SetupExecuteAsync<InsertStudentCourseScheduled>(OneRowUpdated);
            SetupGetResponseAsync<GetStudentCourseScheduledRequest, StudentCourseScheduled>(expected);

            Assert.Equal(expected, await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task InsertStudentCourseScheduled_Given_NoRowsUpdated_AndStudentCourseScheduledAlreadyExisting_ShouldThrow_AlreadyExistsException()
        {
            SetupExecuteAsync<InsertStudentCourseScheduled>(NoRowsUpdated);
            SetupFetchAsync<IsStudentCourseScheduledExisting, bool>(true);

            await Assert.ThrowsAsync<AlreadyExistsException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task InsertStudentCourseScheduled_Given_NoRowsUpdated_AndStudentNotExisting_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<InsertStudentCourseScheduled>(NoRowsUpdated);
            SetupFetchAsync<IsStudentCourseScheduledExisting, bool>(false);
            SetupFetchAsync<IsStudentCodeExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task InsertStudentCourseScheduled_Given_NoRowsUpdated_AndCourseScheduledNotExisting_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<InsertStudentCourseScheduled>(NoRowsUpdated);
            SetupFetchAsync<IsStudentCourseScheduledExisting, bool>(false);
            SetupFetchAsync<IsStudentCodeExisting, bool>(true);
            SetupFetchAsync<IsCourseScheduledGuidExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task InsertStudentCourseScheduled_Given_NoRowsUpdated_AndCourseScheduledNotAlreadyExisting_AndStudentAndInstructorAreExisting_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<InsertStudentCourseScheduled>(NoRowsUpdated);
            SetupFetchAsync<IsStudentCourseScheduledExisting, bool>(false);
            SetupFetchAsync<IsStudentCodeExisting, bool>(true);
            SetupFetchAsync<IsCourseScheduledGuidExisting, bool>(true);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
