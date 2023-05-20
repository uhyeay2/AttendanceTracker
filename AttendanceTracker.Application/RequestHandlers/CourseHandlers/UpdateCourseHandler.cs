using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Application.RequestHandlers.CourseHandlers
{
    public class UpdateCourseRequest : RequiredCodeRequest
    {
        public string? Name { get; set; }
    }

    internal class UpdateCourseHandler : DataHandler<UpdateCourseRequest>
    {
        public UpdateCourseHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task HandleRequestAsync(UpdateCourseRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new UpdateCourse(request.Code, request.Name));

            if (rowsAffected.NoRowsAreUpdated())
            {
                throw await _dataAccess.FetchAsync(new IsCourseCodeExisting(request.Code)) ?
                    new ExpectationFailedException(nameof(UpdateCourseRequest))
                    : new DoesNotExistException(typeof(Course), request.Code, nameof(request.Code));
            }
        }
    }
}
