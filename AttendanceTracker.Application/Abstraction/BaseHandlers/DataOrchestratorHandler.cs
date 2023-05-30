namespace AttendanceTracker.Application.Abstraction.BaseHandlers
{
    internal abstract class DataOrchestratorHandler<TRequest, TResponse> : DataHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly IOrchestrator _orchestrator;

        protected DataOrchestratorHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess) => _orchestrator = orchestrator;
    }

    //internal abstract class DataOrchestratorHandler<TRequest> : DataHandler<TRequest> where TRequest : IRequest
    //{

    //    protected readonly IOrchestrator _orchestrator;

    //    protected DataOrchestratorHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess) => _orchestrator = orchestrator;
    //}
}
