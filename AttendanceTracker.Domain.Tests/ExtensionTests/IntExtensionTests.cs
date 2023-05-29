using AttendanceTracker.Domain.Extensions;

namespace AttendanceTracker.Domain.Tests.ExtensionTests
{
    public class IntExtensionTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(200)]
        public void NoRowsAreUpdated_Given_NumberGreaterThanZero_ShouldReturn_False(int rowsAffected)
        {
            Assert.False(rowsAffected.NoRowsAreUpdated());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-5)]
        [InlineData(-10)]
        [InlineData(-200)]
        public void NoRowsAreUpdated_Given_NumberLessThanOne_ShouldReturn_True(int rowsAffected)
        {
            Assert.True(rowsAffected.NoRowsAreUpdated());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(200)]
        public void AnyRowsAreUpdated_Given_NumberGreaterThanZero_ShouldReturn_True(int rowsAffected)
        {
            Assert.True(rowsAffected.AnyRowsAreUpdated());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-5)]
        [InlineData(-10)]
        [InlineData(-200)]
        public void AnyRowsAreUpdated_Given_NumberLessThanOne_ShouldReturn_False(int rowsAffected)
        {
            Assert.False(rowsAffected.AnyRowsAreUpdated());
        }
    }
}
