using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;
using AttendanceTracker.Domain.Constants;

namespace AttendanceTracker.Application.RequestHandlers.SubjectHandlers
{
    public class InsertSubjectRequest : IRequest<Subject>, IValidatable
    {
        public InsertSubjectRequest() { }

        public InsertSubjectRequest(string subjectCode, string name)
        {
            SubjectCode = subjectCode;
            Name = name;
        }

        public string SubjectCode { get; set; } = null!;
        public string Name { get; set; } = null!;

        public bool IsValid(out List<string> validationFailures) =>
            Validation.Initialize(out validationFailures)
                .AddFailureIfOutsideRange(SubjectCode, nameof(SubjectCode), SubjectCodeConstants.MinLength, SubjectCodeConstants.MaxLength)
                .AddFailureIfAnyCharactersAreNotLetters(SubjectCode, nameof(SubjectCode))
                .AddFailureIfNullOrWhiteSpace(Name, nameof(Name))
            .IsValidWhenNoFailures();
    }

    internal class InsertSubjectHandler : DataHandler<InsertSubjectRequest, Subject>
    {
        public InsertSubjectHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<Subject> HandleRequestAsync(InsertSubjectRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertSubject(request.SubjectCode, request.Name));

            if (rowsAffected.AnyRowsAreUpdated())
            {
                var dto = await _dataAccess.FetchAsync(new GetSubjectByCode(request.SubjectCode));

                return dto.AsSubject();
            }

            throw await _dataAccess.FetchAsync(new IsSubjectCodeExisting(request.SubjectCode)) ? 
                new AlreadyExistsException(typeof(Subject), (request.SubjectCode, nameof(request.SubjectCode)))
                : new ExpectationFailedException(nameof(InsertSubjectRequest));
        }
    }
}
