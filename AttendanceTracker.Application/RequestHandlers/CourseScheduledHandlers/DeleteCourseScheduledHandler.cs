using AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests;

namespace AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers
{
    public class DeleteCourseScheduledRequest : RequiredGuidRequest { }

    internal class DeleteCourseScheduledHandler : DataHandler<DeleteCourseScheduledRequest>
    {
        public DeleteCourseScheduledHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task HandleRequestAsync(DeleteCourseScheduledRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteCourseScheduled(request.Guid));

            if (rowsAffected.NoRowsAreUpdated())
            {
                throw await _dataAccess.FetchAsync(new IsCourseScheduledGuidExisting(request.Guid)) ?
                    new ExpectationFailedException(nameof(DeleteCourseScheduledRequest))
                    : new DoesNotExistException(typeof(CourseScheduled), (request.Guid, nameof(request.Guid)));
            }
        }
    }
}
