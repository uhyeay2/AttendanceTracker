using AttendanceTracker.Application.RequestHandlers.CourseHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.CourseHandlerTests
{
    public class DeleteCourseHandlerTests : HandlerTest
    {
        private readonly DeleteCourseHandler _handler;

        public DeleteCourseHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task DeleteCourse_Given_ExecuteAsync_ReturnsRowsUpdated_ShouldNot_ThrowException()
        {
            SetupExecuteAsync<DeleteCourse>(OneRowUpdated);

            Assert.Null(await Record.ExceptionAsync(async () => await _handler.HandleRequestAsync(new())));
        }

        [Fact]
        public async Task DeleteCourse_Given_ExecuteAsync_ReturnsNoRowsUpdated_AndCourseCodeExists_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<DeleteCourse>(NoRowsUpdated);
            SetupFetchAsync<IsCourseCodeExisting, bool>(true);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task DeleteCourse_Given_ExecuteAsync_ReturnsNoRowsUpdated_AndCourseCodeNotExists_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<DeleteCourse>(NoRowsUpdated);
            SetupFetchAsync<IsCourseCodeExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
