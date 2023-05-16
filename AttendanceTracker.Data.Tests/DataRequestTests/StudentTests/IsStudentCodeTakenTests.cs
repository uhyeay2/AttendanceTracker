namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentTests
{
    public class IsStudentCodeTakenTests : DataTest
    {
        [Fact]
        public async Task IsStudentCodeTaken_Given_StudentCodeNotTaken_ShouldReturn_False()
        {
            Assert.False(await _dataAccess.FetchAsync(_requestFactory.IsStudentCodeTaken()));
        }

        [Fact]
        public async Task IsStudentCodeTaken_Given_StudentCodeIsTaken_ShouldReturn_True()
        {
            await _dataAccess.ExecuteAsync(_requestFactory.InsertStudent());

            Assert.True(await _dataAccess.FetchAsync(_requestFactory.IsStudentCodeTaken()));

            await _dataAccess.ExecuteAsync(_requestFactory.DeleteStudent());
        }
    }
}
