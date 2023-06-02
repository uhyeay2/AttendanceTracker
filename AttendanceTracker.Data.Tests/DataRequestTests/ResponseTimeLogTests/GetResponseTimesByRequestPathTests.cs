using AttendanceTracker.Data.DataRequestObjects.LogRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.ResponseTimeLogTests
{
    public class GetResponseTimesByRequestPathTests : DataTest
    {
        [Fact]
        public async Task GetResponseTimesByRequestPath_Given_SingleRequestPath_ShouldReturn_ResponseTimesForThatPath()
        {
            var responseTimeLog = new InsertResponseTimeLog(new DateTime(2023, 5, 20, 12, 30, 30), RandomString(), 12345);

            await _dataAccess.ExecuteAsync(responseTimeLog);

            var result = await _dataAccess.FetchAsync(new GetResponseTimesByRequestPath(responseTimeLog.RequestPath));

            await _dataAccess.ExecuteAsync(new DeleteResponseTimeLog(result.Id));

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(responseTimeLog.DateTimeRequestWasReceivedInUTC, result.DateTimeRequestWasReceivedInUTC);
                Assert.Equal(responseTimeLog.ResponseTimeInMilliseconds, result.ResponseTimeInMilliseconds);
                Assert.Equal(responseTimeLog.RequestPath, result.RequestPath);
            });
        }

        [Fact]
        public async Task GetResponseTimesByRequestPath_Given_MultipleRequestPaths_ShouldReturn_ResponseTimesForBothPaths()
        {
            var firstLog = new InsertResponseTimeLog(new DateTime(2023, 5, 20, 12, 30, 30), RandomString(), 12345);
            var secondLog= new InsertResponseTimeLog(new DateTime(2023, 4, 10, 11, 15, 15), RandomString(), 54321);

            await _dataAccess.ExecuteAsync(firstLog);
            await _dataAccess.ExecuteAsync(secondLog);

            var result = await _dataAccess.FetchListAsync(new GetResponseTimesByRequestPath(new List<string>() { firstLog.RequestPath, secondLog.RequestPath }));

            foreach (var log in result)
            {
                await _dataAccess.ExecuteAsync(new DeleteResponseTimeLog(log.Id));
            }

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Contains(firstLog.RequestPath, result.Select(_ => _.RequestPath));
                Assert.Contains(secondLog.RequestPath, result.Select(_ => _.RequestPath));
            });
        }
    }
}
