using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Tests.Shared.DataSeeder
{
    public class SeedSubjectRequest : DataSeederRequest<Subject_DTO>
    {
        public SeedSubjectRequest(string? subjectCode = null, string? name = null)
        {
            SubjectCode = subjectCode;
            Name = name;
        }

        public string? SubjectCode { get; set; }
        public string? Name { get; set; }

        public override async Task<Subject_DTO> ExecuteAsync(DataSeeder dataSeeder)
        {
            if (string.IsNullOrWhiteSpace(SubjectCode)) SubjectCode = _randomStringFactory.RandomStringLettersOrNumbers(SubjectCodeConstants.MaxLength);

            if (string.IsNullOrWhiteSpace(Name)) Name = _randomStringFactory.RandomStringLettersOrNumbers();

            return await dataSeeder.SeedFetchAndQueueForDeletionAsync(
                new InsertSubject(SubjectCode, Name),
                new GetSubjectByCode(SubjectCode),
                new DeleteSubject(SubjectCode)
            );
        }
    }
}
