using AttendanceTracker.Application.RequestHandlers.CourseHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.CourseHandlerTests
{
    public class UpdateCourseHandlerTests : HandlerTest
    {
        private readonly UpdateCourseHandler _handler;

        public UpdateCourseHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task UpdateCourse_Given_RowIsUpdated_ShouldNot_ThrowException()
        {
            SetupExecuteAsync<UpdateCourse>(OneRowUpdated);

            Assert.Null(await Record.ExceptionAsync(async () => await _handler.HandleRequestAsync(new())));
        }

        [Fact]
        public async Task UpdateCourse_Given_NoRowsUpdated_And_CourseCodeNotTaken_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<UpdateCourse>(NoRowsUpdated);
            SetupFetchAsync<IsCourseCodeExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task UpdateCourse_Given_NoRowsUpdated_And_CourseCodeIsTaken_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<UpdateCourse>(NoRowsUpdated);
            SetupFetchAsync<IsCourseCodeExisting, bool>(true);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
