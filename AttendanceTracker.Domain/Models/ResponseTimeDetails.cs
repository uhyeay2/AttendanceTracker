namespace AttendanceTracker.Domain.Models
{

    public class OverallResponseTimeDetails : ResponseTimeDetails
    {
        public OverallResponseTimeDetails() { }

        public OverallResponseTimeDetails(IEnumerable<RequestResponseTimeDetails> requests)
        {
            Requests = requests;

            if (requests.Any())
            {
                CountOfRequests = requests.Sum(_ => _.CountOfRequests);
                AverageResponseTime = requests.Average(_ => _.AverageResponseTime);
                ShortestResponseTime = requests.Min(_ => _.ShortestResponseTime);
                LongestResponseTime = requests.Max(_ => _.LongestResponseTime);
            }
        }

        public IEnumerable<RequestResponseTimeDetails> Requests { get; set; } = Enumerable.Empty<RequestResponseTimeDetails>();
    }

    public class RequestResponseTimeDetails : ResponseTimeDetails
    {
        public RequestResponseTimeDetails() { }

        public RequestResponseTimeDetails(string nameOfRequest, int countOfRequests, double averageResponseTime, long longestResponseTime, long shortestResponseTime) 
        : base(countOfRequests, averageResponseTime, longestResponseTime, shortestResponseTime)
        {
            NameOfRequest = nameOfRequest;
        }

        public string NameOfRequest { get; set; } = string.Empty;
    }

    public class ResponseTimeDetails
    {
        public ResponseTimeDetails() { }

        public ResponseTimeDetails(int countOfRequests, double averageResponseTime, long longestResponseTime, long shortestResponseTime)
        {
            CountOfRequests = countOfRequests;
            AverageResponseTime = averageResponseTime;
            LongestResponseTime = longestResponseTime;
            ShortestResponseTime = shortestResponseTime;
        }

        public int CountOfRequests { get; set; }

        public double AverageResponseTime { get; set; }

        public long LongestResponseTime { get; set; }

        public long ShortestResponseTime { get; set; }
    }

}
