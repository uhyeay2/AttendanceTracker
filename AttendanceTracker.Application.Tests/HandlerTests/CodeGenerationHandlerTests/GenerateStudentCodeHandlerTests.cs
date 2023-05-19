using AttendanceTracker.Application.RequestHandlers.CodeGenerationHandlers;
using AttendanceTracker.Domain.Constants;
using AttendanceTracker.Domain.Factories;

namespace AttendanceTracker.Application.Tests.HandlerTests.CodeGenerationHandlerTests
{
    public class GenerateStudentCodeHandlerTests
    {
        private readonly GenerateStudentCodeHandler _handler = new(new RandomCharacterFactory());

        [Fact]
        public void GenerateStudentCode_Should_GenerateCode_WithExpectedLength()
        {
            var code = _handler.HandleRequest(new());

            Assert.Equal(StudentCodeConstants.ExpectedLength, code.Length);
        }

        [Fact]
        public void GenerateStudentCode_Should_GenerateCode_WithExpectedLength_OfLeadingLetters()
        {
            var code = _handler.HandleRequest(new());

            var leadingCharacters = code.Take(StudentCodeConstants.LengthOfLeadingLetters);

            var endingCharacters = code.Skip(StudentCodeConstants.LengthOfLeadingLetters);

            Assert.Multiple(() =>
            {
                Assert.All(leadingCharacters, _ => char.IsLetter(_));

                Assert.DoesNotContain(endingCharacters, _ => char.IsLetter(_));
            });
        }

        [Fact]
        public void GenerateStudentCode_Should_GenerateCode_WithExpectedLength_OfEndingNumbers()
        {
            var code = _handler.HandleRequest(new());

            var leadingCharacters = code.Take(StudentCodeConstants.LengthOfLeadingLetters);

            var endingCharacters = code.Skip(StudentCodeConstants.LengthOfLeadingLetters);

            Assert.Multiple(() =>
            {
                Assert.DoesNotContain(leadingCharacters, _ => char.IsDigit(_));

                Assert.All(endingCharacters, _ => char.IsDigit(_));
            });
        }

        [Fact]
        public void GenerateStudentCode_Should_ReturnRandomCodes_NotMatching_WhenCalledManyTimes()
        {
            var numberOfCodesToGenerate = 100;

            var codes = new string[numberOfCodesToGenerate];

            for (int i = 0; i < numberOfCodesToGenerate; i++)
            {
                codes[i] = _handler.HandleRequest(new());
            }

            var countsOfMatches = codes.Select(c => codes.Count(x => c == x));

            Assert.True(countsOfMatches.All(_ => _ == 1));
        }
    }
}
