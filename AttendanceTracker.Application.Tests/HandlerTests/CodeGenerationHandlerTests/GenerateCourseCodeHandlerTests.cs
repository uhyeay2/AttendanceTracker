using AttendanceTracker.Application.RequestHandlers.CodeGenerationHandlers;
using AttendanceTracker.Domain.Constants;

namespace AttendanceTracker.Application.Tests.HandlerTests.CodeGenerationHandlerTests
{
    public class GenerateCourseCodeHandlerTests
    {
        private readonly GenerateCourseCodeHandler _handler = new();

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

            var endingDigits = generatedCode.Skip(subjectCode.Length);

            Assert.Multiple(() =>
            {
                Assert.All(endingDigits, _ => char.IsDigit(_));

                Assert.Equal(CourseCodeConstants.NumberOfEndingDigits, endingDigits.Count());
            });
        }

        [Fact]
        public void GenerateCourseCode_Given_SubjectCode_LengthTooLong_Should_SubStringSubjectCode()
        {
            var longSubjectCode = "Really Long Subject Code Exceeding Maximum Length";

            var generatedCode = _handler.HandleRequest(new(longSubjectCode));

            Assert.Multiple(() =>
            {

                Assert.Equal(CourseCodeConstants.MaxLength, generatedCode.Length);

                Assert.All(generatedCode.TakeLast(CourseCodeConstants.NumberOfEndingDigits), _ => char.IsDigit(_));
            });
        }
    }
}
