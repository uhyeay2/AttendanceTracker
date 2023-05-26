using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Application.RequestHandlers.SubjectHandlers
{
    public class DeleteSubjectRequest : RequiredCodeRequest
    {
        public DeleteSubjectRequest() { }

        public DeleteSubjectRequest(string code) : base(code) { }
    }

    internal class DeleteSubjectHandler : DataHandler<DeleteSubjectRequest>
    {
        public DeleteSubjectHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task HandleRequestAsync(DeleteSubjectRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteSubject(request.Code));

            if (rowsAffected.NoRowsAreUpdated())
            {
                throw await _dataAccess.FetchAsync(new IsSubjectCodeExisting(request.Code)) ?
                    new ExpectationFailedException(nameof(DeleteSubjectRequest))
                    : new DoesNotExistException(typeof(Subject), (request.Code, nameof(request.Code)));
            }
        }
    }
}
