using AttendanceTracker.Application.RequestHandlers.CodeGenerationHandlers;
using AttendanceTracker.Domain.Constants;
using AttendanceTracker.Domain.Factories;

namespace AttendanceTracker.Application.Tests.HandlerTests.CodeGenerationHandlerTests
{
    public class GenerateCourseCodeHandlerTests
    {
        private readonly GenerateCourseCodeHandler _handler = new(new RandomCharacterFactory());

        public static readonly IEnumerable<object[]> TestSubjectCodes = new[] 
        {
           new[] { "MAT" },  new[] { "HIS" }, new [] { "ENG" } 
        };

        [Theory]
        [MemberData(nameof(TestSubjectCodes))]
        public void GenerateCourseCode_Given_SubjectCode_Should_GenerateCode_StartingWithSubjectCode(string subjectCode)
        {
            var generatedCode = _handler.HandleRequest(new(subjectCode));

            Assert.StartsWith(subjectCode, generatedCode);
        }

        [Theory]
        [MemberData(nameof(TestSubjectCodes))]
        public void GenerateCourseCode_Given_SubjectCode_Should_GenerateCode_With_ExpectedLength_EndingNumbers(string subjectCode)
        {
            var generatedCode = _handler.HandleRequest(new(subjectCode));

            var endingDigits = generatedCode.TakeLast(CourseCodeConstants.CountOfEndingNumbers);

            Assert.All(endingDigits, _ => char.IsDigit(_));
        }
    }
}
