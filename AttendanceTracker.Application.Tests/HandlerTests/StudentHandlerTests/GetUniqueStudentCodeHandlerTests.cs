using AttendanceTracker.Application.RequestHandlers.CodeGenerationHandlers;
using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Domain.Constants;
using Moq;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentHandlerTests
{
    public class GetUniqueStudentCodeHandlerTests : HandlerTest
    {
        private readonly GetUniqueStudentCodeHandler _handler;

        public GetUniqueStudentCodeHandlerTests() => _handler = new(_mockDataAccess.Object, _mockOrchestrator.Object);

        [Fact]
        public async Task GetUniqueStudentCode_Given_GeneratedCodeIsNotTaken_ShouldReturn_CodeGenerated()
        {
            var expectedCode = "GeneratedCode";

            SetupGetResponse<GenerateStudentCodeRequest, string>(expectedCode);
            SetupFetchAsync<IsStudentCodeExisting, bool>(false);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Equal(expectedCode, result);
        }

        [Fact]
        public async Task GetUniqueStudentCode_Given_GeneratedCodeIsTaken_ShouldThrow_ExpectationFailedException_AfterMaxAttempts()
        {
            SetupFetchAsync<IsStudentCodeExisting, bool>(true);

            var exception = await Record.ExceptionAsync(async () => await _handler.HandleRequestAsync(new()));

            Assert.Multiple(() =>
            {
                Assert.IsType<ExpectationFailedException>(exception);

                Assert.EndsWith(StudentCodeConstants.MaxAttemptsExceededErrorMessage, exception.Message);

                _mockDataAccess.Verify(_ => _.FetchAsync(It.IsAny<IsStudentCodeExisting>()), 
                          Times.Exactly(StudentCodeConstants.MaxAttemptsToGenerate));
            });
        }
    }
}
