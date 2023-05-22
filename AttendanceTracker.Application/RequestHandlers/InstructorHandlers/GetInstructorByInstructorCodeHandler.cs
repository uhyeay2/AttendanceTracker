using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Application.RequestHandlers.InstructorHandlers
{
    public class GetInstructorByInstructorCodeRequest : RequiredCodeRequest<Instructor> { }

    internal class GetInstructorByInstructorCodeHandler : DataHandler<GetInstructorByInstructorCodeRequest, Instructor>
    {
        public GetInstructorByInstructorCodeHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<Instructor> HandleRequestAsync(GetInstructorByInstructorCodeRequest request)
        {
            var dto = await _dataAccess.FetchAsync(new GetInstructorByCode(request.Code));

            return dto == null
                ? throw new DoesNotExistException(typeof(Instructor), request.Code, nameof(request.Code))
                : new Instructor(dto.InstructorCode, dto.FirstName, dto.LastName);
        }
    }
}
