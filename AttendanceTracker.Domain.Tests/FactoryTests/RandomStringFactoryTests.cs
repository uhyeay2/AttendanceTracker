using AttendanceTracker.Domain.Factories;
using AttendanceTracker.Domain.Interfaces;
using Moq;

namespace AttendanceTracker.Domain.Tests.FactoryTests
{
    public class RandomStringFactoryTests
    {
        private readonly RandomStringFactory _factory;

        private readonly Mock<IRandomCharacterFactory> _mockRandomCharacterFactory = new();

        private const int _expectedLength = 5;

        private const char _mockCharacterGenerated = 'A';

        public RandomStringFactoryTests() => _factory = new(_mockRandomCharacterFactory.Object);

        [Fact]
        public void GetRandomStringLettersOnly_Given_Length_Should_ReturnString_WithExpectedCharacterAndLength()
        {
            _mockRandomCharacterFactory.Setup(_ => _.GetRandomLetter()).Returns(_mockCharacterGenerated);

            var expected = new string(_mockCharacterGenerated, _expectedLength);

            var actual = _factory.RandomStringLettersOnly(_expectedLength);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetRandomStringNumbersOnly_Given_Length_Should_ReturnString_WithExpectedCharacterAndLength()
        {
            _mockRandomCharacterFactory.Setup(_ => _.GetRandomNumber()).Returns(_mockCharacterGenerated);

            var expected = new string(_mockCharacterGenerated, _expectedLength);

            var actual = _factory.RandomStringNumbersOnly(_expectedLength);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetRandomStringLettersOrNumbers_Given_Length_Should_ReturnString_WithExpectedCharacterAndLength()
        {
            _mockRandomCharacterFactory.Setup(_ => _.GetRandomLetterOrNumber()).Returns(_mockCharacterGenerated);

            var expected = new string(_mockCharacterGenerated, _expectedLength);

            var actual = _factory.RandomStringLettersOrNumbers(_expectedLength);

            Assert.Equal(expected, actual);
        }
    }
}
