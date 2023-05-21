using AttendanceTracker.Data.Abstraction.Interfaces;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;
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
        private readonly List<string> _seededCourseCodes = new();
        private readonly List<string> _seededSubjectCodes = new();

        public async Task PurgeSeededRecords()
        {
            foreach (var studentCode in _seededStudentCodes)
            {
                await _dataAccess.ExecuteAsync(new DeleteStudent(studentCode));
            }

            foreach (var courseCode in _seededCourseCodes)
            {
                await _dataAccess.ExecuteAsync(new DeleteCourse(courseCode));
            }

            foreach (var subjectCode in _seededSubjectCodes)
            {
                await _dataAccess.ExecuteAsync(new DeleteSubject(subjectCode));
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

        public async Task<Subject_DTO> NewSubject(string? subjectCode = null, string? name = null)
        {
            var randomSubject = A.New<Subject_DTO>();

            subjectCode ??= Guid.NewGuid().ToString()[..10];

            await _dataAccess.ExecuteAsync(new InsertSubject(subjectCode, name ?? randomSubject.Name));

            _seededSubjectCodes.Add(subjectCode);

            return await _dataAccess.FetchAsync(new GetSubjectByCode(subjectCode));
        }

        public async Task<Course_DTO> NewCourse(string? courseCode = null, string? name = null)
        {
            var randomCourse = A.New<Course_DTO>();

            courseCode ??= Guid.NewGuid().ToString()[..CourseCodeConstants.MaxLength]; ;

            await _dataAccess.ExecuteAsync(new InsertCourse(courseCode, name ?? randomCourse.Name));

            _seededCourseCodes.Add(courseCode!);

            return await _dataAccess.FetchAsync(new GetCourseByCourseCode(courseCode));
        }
    }
}
