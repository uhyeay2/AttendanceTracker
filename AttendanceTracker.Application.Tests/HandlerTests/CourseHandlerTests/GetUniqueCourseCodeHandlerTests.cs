using AttendanceTracker.Application.RequestHandlers.CodeGenerationHandlers;
using AttendanceTracker.Application.RequestHandlers.CourseHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Domain.Constants;
using Moq;

namespace AttendanceTracker.Application.Tests.HandlerTests.CourseHandlerTests
{
    public class GetUniqueCourseCodeHandlerTests : HandlerTest
    {
        private readonly GetUniqueCourseCodeHandler _handler;

        public GetUniqueCourseCodeHandlerTests() => _handler = new(_mockDataAccess.Object, _mockOrchestrator.Object);

        [Fact]
        public async Task GetUniqueCourseCode_Given_GeneratedCodeIsNotTaken_ShouldReturn_CodeGenerated()
        {
            var expectedCode = "GeneratedCode";

            SetupGetResponse<GenerateCourseCodeRequest, string>(expectedCode);
            SetupFetchAsync<IsCourseCodeExisting, bool>(false);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Equal(expectedCode, result);
        }

        [Fact]
        public async Task GetUniqueCourseCode_Given_GeneratedCodeIsTaken_ShouldThrow_ExpectationFailedException_AfterMaxAttempts()
        {
            SetupFetchAsync<IsCourseCodeExisting, bool>(true);

            var exception = await Record.ExceptionAsync(async () => await _handler.HandleRequestAsync(new()));

            Assert.Multiple(() =>
            {
                Assert.IsType<ExpectationFailedException>(exception);

                Assert.EndsWith(CourseCodeConstants.MaxAttemptsExceededErrorMessage, exception.Message);

                _mockDataAccess.Verify(_ => _.FetchAsync(It.IsAny<IsCourseCodeExisting>()),
                          Times.Exactly(CourseCodeConstants.MaxAttemptsToGenerate));
            });
        }
    }
}
