namespace AttendanceTracker.Api.Tests.TestHelpers
{
    public static class TestCases
    {
        public static readonly IEnumerable<object[]> NullEmptyAndWhitespaceString = new[]
        {
            new object[] { "" },
            new object[] { " " },
            new object[] { null! }
        };
    }
}
