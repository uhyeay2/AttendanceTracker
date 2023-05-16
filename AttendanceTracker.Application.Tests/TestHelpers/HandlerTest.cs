using AttendanceTracker.Data.Abstraction.Interfaces;
using Moq;

namespace AttendanceTracker.Application.Tests.TestHelpers
{
    public abstract class HandlerTest
    {
        protected readonly Mock<IDataAccess> _mockDataAccess;

        protected const int OneRowUpdated = 1;

        protected const int NoRowsUpdated = 0;

        public HandlerTest()
        {
            _mockDataAccess = new();
        }

        #region Helpers for Setting up FetchListAsync through mocked DataAccess

        /// <summary>
        /// Setup IDataAccess.FetchListAsync() given any Type of TRequest will return response provided.
        /// </summary>
        protected void SetupFetchListAsync<TRequest, TResponse>(IEnumerable<TResponse> response) where TRequest : IDataRequest<TResponse> => 
            _mockDataAccess.Setup(_ => _.FetchListAsync(It.IsAny<TRequest>())).Returns(Task.FromResult(response));

        /// <summary>
        /// Setup IDataAccess.FetchListAsync() given request provided will return response provided.
        /// </summary>
        protected void SetupFetchListAsync<TRequest, TResponse>(TRequest request, IEnumerable<TResponse> response) where TRequest : IDataRequest<TResponse> =>
            _mockDataAccess.Setup(_ => _.FetchListAsync(request)).Returns(Task.FromResult(response));

        #endregion

        #region Helpers for Setting up FetchAsync through mocked DataAccess

        /// <summary>
        /// Setup IDataAccess.FetchAsync() given any Type of TRequest will return the response provided.
        /// </summary>
        protected void SetupFetchAsync<TRequest, TResponse>(TResponse response) where TRequest : IDataRequest<TResponse> =>
            _mockDataAccess.Setup(_ => _.FetchAsync(It.IsAny<TRequest>())).Returns(Task.FromResult(response));

        /// <summary>
        /// Setup IDataAccess.FetchAsync() given request provided will return response provided.
        /// </summary>
        protected void SetupFetchAsync<TRequest, TResponse>(TRequest request, TResponse response) where TRequest : IDataRequest<TResponse> =>
            _mockDataAccess.Setup(_ => _.FetchAsync(request)).Returns(Task.FromResult(response));

        #endregion

        #region Helpers for setting up ExecuteAsync through mocked DataAccess

        /// <summary>
        /// Setup IDataAccess.ExecuteAsync() given any Type of TRequest will return the int provided.
        /// </summary>
        protected void SetupExecuteAsync<TRequest>(int response) where TRequest : IDataRequest =>
            _mockDataAccess.Setup(_ => _.ExecuteAsync(It.IsAny<TRequest>())).Returns(Task.FromResult(response));

        #endregion
    }
}
