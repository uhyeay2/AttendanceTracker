using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentHandlers
{
    public class DeleteStudentRequest : RequiredCodeRequest { }

    internal class DeleteStudentHandler : DataHandler<DeleteStudentRequest>
    {
        public DeleteStudentHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task HandleRequestAsync(DeleteStudentRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteStudent(request.Code));

            if (rowsAffected.NoRowsAreUpdated())
            {
                throw await _dataAccess.FetchAsync(new IsStudentCodeExisting(request.Code)) ?
                    new ExpectationFailedException(nameof(DeleteStudentRequest))
                    : new DoesNotExistException(typeof(Student), (request.Code, nameof(request.Code)));
            }
        }
    }
}
