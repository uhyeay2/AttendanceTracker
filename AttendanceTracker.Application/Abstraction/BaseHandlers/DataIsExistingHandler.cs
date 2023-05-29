namespace AttendanceTracker.Application.Abstraction.BaseHandlers
{
    internal abstract class DataIsExistingHandler<TRequest> : DataHandler<TRequest, bool> where TRequest : IRequest<bool>
    {
        protected DataIsExistingHandler(IDataAccess dataAccess) : base(dataAccess) { }

        protected abstract IDataRequest<bool> InitializeFetchRequest(TRequest request);

        public override async Task<bool> HandleRequestAsync(TRequest request) => await _dataAccess.FetchAsync(InitializeFetchRequest(request));
    }
}
