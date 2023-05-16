namespace AttendanceTracker.Application.RequestHandlers.ExampleHandlers
{
    public class GetSumRequest : IRequest<int>
    {
        public int FirstNumber { get; set; }

        public int SecondNumber { get; set; }
    }

    internal class GetSumHandler : ITaskHandler<GetSumRequest, int>
    {
        public Task<int> HandleRequestAsync(GetSumRequest request)
        {
            return Task.FromResult(request.FirstNumber + request.SecondNumber);
        }
    }
}
