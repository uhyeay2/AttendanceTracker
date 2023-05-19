using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentHandlers
{
    public class DeleteStudentRequest : RequiredStudentCodeRequest { }

    internal class DeleteStudentHandler : DataHandler<DeleteStudentRequest>
    {
        public DeleteStudentHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task HandleRequestAsync(DeleteStudentRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteStudent(request.StudentCode));

            if (rowsAffected.NoRowsAreUpdated())
            {
                throw await _dataAccess.FetchAsync(new IsStudentCodeTaken(request.StudentCode)) ?
                    new ExpectationFailedException(nameof(DeleteStudentRequest))
                    : new DoesNotExistException(typeof(Student), (request.StudentCode, nameof(request.StudentCode)));
            }
        }
    }
}
