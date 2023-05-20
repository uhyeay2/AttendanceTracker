using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Application.RequestHandlers.CourseHandlers
{
    public class DeleteCourseRequest : RequiredCodeRequest { }

    internal class DeleteCourseHandler : DataHandler<DeleteCourseRequest>
    {
        public DeleteCourseHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task HandleRequestAsync(DeleteCourseRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteCourse(request.Code));

            if (rowsAffected.NoRowsAreUpdated())
            {
                throw await _dataAccess.FetchAsync(new IsCourseCodeExisting(request.Code)) ?
                    new ExpectationFailedException(nameof(DeleteCourseRequest))
                    : new DoesNotExistException(typeof(Course), (request.Code, nameof(request.Code)));
            }
        }
    }
}
