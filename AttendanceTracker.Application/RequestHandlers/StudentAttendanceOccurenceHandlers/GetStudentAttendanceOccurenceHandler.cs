using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentAttendanceOccurenceHandlers
{
    public class GetStudentAttendanceOccurenceRequest : RequiredGuidRequest<StudentAttendanceOccurence>
    {
        public GetStudentAttendanceOccurenceRequest() { }

        public GetStudentAttendanceOccurenceRequest(Guid guid) : base(guid) { }
    }

    internal class GetStudentAttendanceOccurenceHandler : DataOrchestratorHandler<GetStudentAttendanceOccurenceRequest, StudentAttendanceOccurence>
    {
        public GetStudentAttendanceOccurenceHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess, orchestrator) { }

        public override async Task<StudentAttendanceOccurence> HandleRequestAsync(GetStudentAttendanceOccurenceRequest request)
        {
            var dto = await _dataAccess.FetchAsync(new GetStudentAttendanceOccurence(request.Guid));

            if (dto == null)
            {
                throw new DoesNotExistException(typeof(StudentAttendanceOccurence), request.Guid, nameof(request.Guid));
            }

            var scheduledCourseDTO = await _dataAccess.FetchAsync(new GetStudentCourseScheduledById(dto.StudentCourseScheduledId));

            var fetchInstructorTask = _dataAccess.FetchAsync(new GetInstructorById(scheduledCourseDTO.InstructorId));
            var fetchCourseTask = _dataAccess.FetchAsync(new GetCourseById(scheduledCourseDTO.CourseId));
            var fetchStudentTask = _dataAccess.FetchAsync(new GetStudentById(scheduledCourseDTO.StudentId));

            await Task.WhenAll(fetchInstructorTask, fetchCourseTask, fetchStudentTask);

            return dto.AsStudentAttendanceOccurence(await fetchStudentTask, scheduledCourseDTO, await fetchCourseTask, await fetchInstructorTask);
        }
    }
}
