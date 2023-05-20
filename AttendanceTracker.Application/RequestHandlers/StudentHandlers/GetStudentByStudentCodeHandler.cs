using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentHandlers
{
    public class GetStudentByStudentCodeRequest : RequiredCodeRequest<Student> { }

    internal class GetStudentByStudentCodeHandler : DataHandler<GetStudentByStudentCodeRequest, Student>
    {
        public GetStudentByStudentCodeHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<Student> HandleRequestAsync(GetStudentByStudentCodeRequest request)
        {
            var dto = await _dataAccess.FetchAsync(new GetStudentByCode(request.Code));

            return dto == null
                ? throw new DoesNotExistException(typeof(Student), request.Code, nameof(request.Code))
                : new Student(dto.StudentCode, dto.FirstName, dto.LastName, dto.DateOfBirth);
        }
    }
}
