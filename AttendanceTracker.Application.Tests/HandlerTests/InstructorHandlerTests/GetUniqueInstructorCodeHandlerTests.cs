using AttendanceTracker.Application.RequestHandlers.CodeGenerationHandlers;
using AttendanceTracker.Application.RequestHandlers.InstructorHandlers;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;
using AttendanceTracker.Domain.Constants;
using Moq;

namespace AttendanceTracker.Application.Tests.HandlerTests.InstructorHandlerTests
{
    public class GetUniqueInstructorCodeHandlerTests : HandlerTest
    {
        private readonly GetUniqueInstructorCodeHandler _handler;

        public GetUniqueInstructorCodeHandlerTests() => _handler = new(_mockDataAccess.Object, _mockOrchestrator.Object);

        [Fact]
        public async Task GetUniqueInstructorCode_Given_GeneratedCodeIsNotTaken_ShouldReturn_CodeGenerated()
        {
            var expectedCode = "GeneratedCode";

            SetupGetResponse<GenerateInstructorCodeRequest, string>(expectedCode);
            SetupFetchAsync<IsInstructorCodeExisting, bool>(false);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Equal(expectedCode, result);
        }

        [Fact]
        public async Task GetUniqueInstructorCode_Given_GeneratedCodeIsTaken_ShouldThrow_ExpectationFailedException_AfterMaxAttempts()
        {
            SetupFetchAsync<IsInstructorCodeExisting, bool>(true);

            var exception = await Record.ExceptionAsync(async () => await _handler.HandleRequestAsync(new()));

            Assert.Multiple(() =>
            {
                Assert.IsType<ExpectationFailedException>(exception);

                Assert.EndsWith(InstructorCodeConstants.MaxAttemptsExceededErrorMessage, exception.Message);

                _mockDataAccess.Verify(_ => _.FetchAsync(It.IsAny<IsInstructorCodeExisting>()),
                          Times.Exactly(InstructorCodeConstants.MaxAttemptsToGenerate));
            });
        }
    }
}
