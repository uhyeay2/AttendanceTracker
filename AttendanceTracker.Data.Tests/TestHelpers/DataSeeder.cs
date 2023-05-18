using AttendanceTracker.Data.Abstraction.Interfaces;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Data.DataTransferObjects;
using AttendanceTracker.Domain.Constants;
using GenFu;

namespace AttendanceTracker.Data.Tests.TestHelpers
{
    public class DataSeeder
    {
        public DataSeeder(IDataAccess dataAccess) => _dataAccess = dataAccess;

        private readonly IDataAccess _dataAccess;
        
        private readonly List<string> _seededStudentCodes = new();

        public async Task PurgeSeededRecords()
        {
            foreach (var studentCode in _seededStudentCodes)
            {
                await _dataAccess.ExecuteAsync(new DeleteStudent(studentCode));
            }
        }

        public async Task<Student_DTO> NewStudent(string? studentCode = null, string? firstName = null, string? lastName = null, DateTime? dateOfBirth = null)
        {
            var randomStudent = A.New<Student_DTO>();

            studentCode ??= Guid.NewGuid().ToString()[..StudentCodeConstants.ExpectedLength];

            await _dataAccess.ExecuteAsync(new InsertStudent(studentCode ?? randomStudent.StudentCode,
                firstName ?? randomStudent.FirstName, lastName ?? randomStudent.LastName, dateOfBirth ?? randomStudent.DateOfBirth));

            _seededStudentCodes.Add(studentCode!);

            return await _dataAccess.FetchAsync(new GetStudentByCode(studentCode!));
        }
    }
}
