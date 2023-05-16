using AttendanceTracker.Domain.Constants;
using AttendanceTracker.Domain.Policy.CodeGeneration;

namespace AttendanceTracker.Domain.Tests.PolicyTests.CodeGenerationTests
{
    public class StudentCodeGenerationTests
    {
        [Fact]
        public void StudentCodeGeneration_Should_ReturnCode_WithExpected_LengthOfLeadingCharacters()
        {
            var code = StudentCodeGeneration.NewCode();

            var leadingLetters = code.Take(StudentCodeConstants.LengthOfLeadingLetters);

            Assert.True(leadingLetters.All(_ => char.IsLetter(_)));
        }

        [Fact]
        public void StudentCodeGeneration_Should_ReturnCode_WithExpected_LengthOfEndingNumbers_AfterLeadingCharacters()
        {
            var codeWithoutLetters = StudentCodeGeneration.NewCode().Skip(StudentCodeConstants.LengthOfLeadingLetters);

            Assert.Multiple(() =>
            {
                Assert.True(codeWithoutLetters.All(_ => char.IsDigit(_)));
                Assert.Equal(StudentCodeConstants.LengthOfEndingNumbers, codeWithoutLetters.Count());
            });
        }

        [Fact]
        public void StudentCodeGeneration_Should_ReturnCode_WithExpectedLength()
        {
            Assert.Equal(StudentCodeConstants.ExpectedLength, StudentCodeGeneration.NewCode().Length);
        }

        [Fact]
        public void StudentCodeGeneration_Should_ReturnRandomCodes_NotMatching_WhenCalledManyTimes()
        {
            var numberOfTimesToCall = 100;

            var results = Enumerable.Range(0, numberOfTimesToCall).Select(_ => StudentCodeGeneration.NewCode()).ToArray();

            var isOnlyOneMatchPerRecord = results.All(r => results.Count(x => r == x) == 1);

            Assert.True(isOnlyOneMatchPerRecord);
        }
    }
}
