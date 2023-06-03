using AttendanceTracker.Application.RequestHandlers.StudentAttendanceOccurenceHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;
using AttendanceTracker.Domain.Models;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentAttendanceOccurenceHandlerTests
{
    public class InsertStudentAttendanceOccurenceHandlerTests : HandlerTest
    {
        private readonly InsertStudentAttendanceOccurenceHandler _handler;

        public InsertStudentAttendanceOccurenceHandlerTests() => _handler = new(_mockDataAccess.Object, _mockOrchestrator.Object);

        [Fact]
        public async Task InsertStudentAttendanceOccurence_Given_RowsUpdated_ShouldReturn_StudentAttendanceOccurence_FetchedFromOrchestrator()
        {
            var expected = A.New<StudentAttendanceOccurence>();

            SetupExecuteAsync<InsertStudentAttendanceOccurence>(OneRowUpdated);
            SetupGetResponseAsync<GetStudentAttendanceOccurenceRequest, StudentAttendanceOccurence>(expected);

            Assert.Equal(expected, await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task InsertStudentAttendanceOccurence_Given_NoRowsUpdated_AndStudentCourseScheduledNotExisting_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<InsertStudentAttendanceOccurence>(NoRowsUpdated);
            SetupFetchAsync<IsStudentCourseScheduledExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task InsertStudentAttendanceOccurence_Given_NoRowsUpdated_AndStudentCourseScheduledExisting_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<InsertStudentAttendanceOccurence>(NoRowsUpdated);
            SetupFetchAsync<IsStudentCourseScheduledExisting, bool>(true);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
