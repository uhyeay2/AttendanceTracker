using AttendanceTracker.Domain.Extensions;

namespace AttendanceTracker.Domain.Tests.ExtensionTests
{
    public class IEnumerableStringExtensionTests
    {
        public static readonly IEnumerable<object[]> EmptyCollectionOfStrings = new[]
        {
            new[] { Enumerable.Empty<string>() },
            new[] { Array.Empty<string>() },
            new[] { new List<string>() },
        };

        [Theory]
        [InlineData(null!)]
        [MemberData(nameof(EmptyCollectionOfStrings))]
        public void AggregateWithCommans_Given_InputIsNullOrEmpty_ShouldReturn_EmptyString(IEnumerable<string> input)
        {
            Assert.Empty(input.AggregateWithCommas());
        }

        [Fact]
        public void AggregateWithCommans_Given_Input_ShouldReturn_AsCommaSeparatedValue()
        {
            var input = new[] {"A", "B", "C", "D", "E", "F", "G"};

            var expected = "A, B, C, D, E, F, G";

            Assert.Equal(expected, input.AggregateWithCommas());
        }

        [Theory]
        [InlineData(null!)]
        [MemberData(nameof(EmptyCollectionOfStrings))]
        public void AsSqlParameters_Given_InputIsNullOrEmpty_ShouldReturn_EmptyString(IEnumerable<string> input)
        {
            Assert.Empty(input.AsSqlParameters());
        }

        [Fact]
        public void AsSqlParameters_Given_Input_ShouldReturn_WithEachStringAppended_WithAtSign()
        {
            var input = new[] { "A", "B", "C", "D", "E", "F", "G" };

            var expected = new[] { "@A", "@B", "@C", "@D", "@E", "@F", "@G" };

            Assert.Equal(expected, input.AsSqlParameters());
        }

        [Theory]
        [InlineData(null!)]
        [MemberData(nameof(EmptyCollectionOfStrings))]
        public void AggregateWithCommasAsColumnsAndSqlParameters_Given_InputIsNullOrEmpty_ShouldReturn_ToupleOfEmptyStrings(IEnumerable<string> input)
        {
            var (columnOutput, parametersOutput) = input.AggregateWithCommasAsColumnsAndSqlParameters();

            Assert.Multiple(() =>
            {
                Assert.Empty(columnOutput);
                Assert.Empty(parametersOutput);
            });
        }

        [Fact]
        public void AggregateWithCommasAsColumnsAndSqlParameters_Given_Input_ShouldReturn_AsTouple_CommaSeparatedValue_AndCommaSeparatedValueEachStringAppendedWithAtSigns()
        {
            var input = new[] { "FirstName", "LastName", "Birthday" };

            var expectedColumns = "FirstName, LastName, Birthday";

            var expectedParameters = "@FirstName, @LastName, @Birthday";

            var (columnOutput, parametersOutput) = input.AggregateWithCommasAsColumnsAndSqlParameters();

            Assert.Multiple(() =>
            {
                Assert.Equal(expectedColumns, columnOutput);
                Assert.Equal(expectedParameters, parametersOutput);
            });
        }
    }
}
