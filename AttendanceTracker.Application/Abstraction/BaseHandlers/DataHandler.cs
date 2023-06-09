﻿namespace AttendanceTracker.Application.Abstraction.BaseHandlers
{
    internal abstract class DataHandler<TRequest, TResponse> : ITaskHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly IDataAccess _dataAccess;

        protected DataHandler(IDataAccess dataAccess) => _dataAccess = dataAccess;

        public abstract Task<TResponse> HandleRequestAsync(TRequest request);
    }

    internal abstract class DataHandler<TRequest> : ITaskHandler<TRequest> where TRequest : IRequest
    {

        protected readonly IDataAccess _dataAccess;

        protected DataHandler(IDataAccess dataAccess) => _dataAccess = dataAccess;

        public abstract Task HandleRequestAsync(TRequest request);
    }
}
