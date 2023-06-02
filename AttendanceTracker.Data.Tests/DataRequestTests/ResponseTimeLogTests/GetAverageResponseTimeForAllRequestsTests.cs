using AttendanceTracker.Data.DataRequestObjects.LogRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.ResponseTimeLogTests
{
    public class GetAverageResponseTimeForAllRequestsTests : DataTest
    {
        private static DateTime RandomDateDuringFirstHalfOfThisYear => new (DateTime.Today.Year, Random.Shared.Next(1, 6), Random.Shared.Next(1, 28), 12, 30, 59);

        [Fact]
        public async Task GetAverageResponseTimeForAllRequests_Given_NoDates_ShouldReturn_AllLogs()
        {
            var requestOnePath = RandomString();
            var requestTwoPath = RandomString();
            var requestsPerPath = 5;

            for (int i = 0; i < requestsPerPath; i++)
            {
                await _dataAccess.ExecuteAsync(new InsertResponseTimeLog(RandomDateDuringFirstHalfOfThisYear, requestOnePath, Random.Shared.Next()));
                await _dataAccess.ExecuteAsync(new InsertResponseTimeLog(RandomDateDuringFirstHalfOfThisYear, requestTwoPath, Random.Shared.Next()));
            }

            var results = await _dataAccess.FetchListAsync(new  GetAverageResponseTimeForAllRequests());

            var logsForDeleting = await _dataAccess.FetchListAsync(
                new GetResponseTimesByRequestPath(new List<string> { requestOnePath, requestTwoPath }));

            foreach (var log in logsForDeleting)
            {
                await _dataAccess.ExecuteAsync(new DeleteResponseTimeLog(log.Id));
            }

            Assert.Multiple(() =>
            {
                Assert.NotEmpty(results);

                Assert.Contains(requestOnePath, results.Select(_ => _.RequestPath));
                Assert.Contains(requestTwoPath, results.Select(_ => _.RequestPath));

                Assert.Equal(requestsPerPath, results.First(_ => _.RequestPath == requestOnePath).CountOfTimesCalled);
                Assert.Equal(requestsPerPath, results.First(_ => _.RequestPath == requestTwoPath).CountOfTimesCalled);
            });
        }

        [Fact]
        public async Task GetAverageResponseTimeForAllRequests_Given_Dates_ShouldReturn_OnlyLogsWithinThatRange()
        {
            var requestOnePath = RandomString();
            var requestTwoPath = RandomString();
            var requestsPerPath = 5;

            var expectedDateTime = new DateTime(DateTime.Today.Year, 8, 1, 12, 30, 59);

            for (int i = 0; i < requestsPerPath; i++)
            {
                await _dataAccess.ExecuteAsync(new InsertResponseTimeLog(RandomDateDuringFirstHalfOfThisYear, requestOnePath, Random.Shared.Next()));
                await _dataAccess.ExecuteAsync(new InsertResponseTimeLog(RandomDateDuringFirstHalfOfThisYear, requestTwoPath, Random.Shared.Next()));
                // for each loop, insert a record for expected DateTime
                await _dataAccess.ExecuteAsync(new InsertResponseTimeLog(expectedDateTime, requestTwoPath, Random.Shared.Next()));
            }

            var results = await _dataAccess.FetchListAsync(new GetAverageResponseTimeForAllRequests(startDate: expectedDateTime, endDate: expectedDateTime));

            var logsForDeleting = await _dataAccess.FetchListAsync(
                new GetResponseTimesByRequestPath(new List<string> { requestOnePath, requestTwoPath }));

            foreach (var log in logsForDeleting)
            {
                await _dataAccess.ExecuteAsync(new DeleteResponseTimeLog(log.Id));
            }

            Assert.Multiple(() =>
            {
                Assert.NotEmpty(results);

                Assert.Contains(requestTwoPath, results.Select(_ => _.RequestPath));

                Assert.Equal(requestsPerPath, results.First(_ => _.RequestPath == requestTwoPath).CountOfTimesCalled);
            });
        }
    }
}
