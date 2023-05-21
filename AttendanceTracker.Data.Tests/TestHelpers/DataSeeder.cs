using AttendanceTracker.Data.Abstraction.Interfaces;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;
using AttendanceTracker.Data.DataTransferObjects;

namespace AttendanceTracker.Data.Tests.TestHelpers
{
    public class DataSeeder
    {
        public DataSeeder(IDataAccess dataAccess) => _dataAccess = dataAccess;

        private readonly IDataAccess _dataAccess;
        
        private readonly List<IDataRequest> _deleteSeededRecordRequests = new();

        public async Task PurgeSeededRecords()
        {
            foreach (var request in _deleteSeededRecordRequests)
            {
                await _dataAccess.ExecuteAsync(request);
            }
        }
       
        public async Task<Student_DTO> NewStudentAsync(string studentCode, string firstName, string lastName, DateTime dateOfBirth) =>
            await SeedFetchAndQueueForDeletion(
                new InsertStudent(studentCode, firstName, lastName, dateOfBirth), new GetStudentByCode(studentCode!), new DeleteStudent(studentCode!));

        public async Task<Subject_DTO> NewSubjectAsync(string subjectCode, string name) =>
            await SeedFetchAndQueueForDeletion( 
                new InsertSubject(subjectCode, name), new GetSubjectByCode(subjectCode), new DeleteSubject(subjectCode));

        public async Task<Course_DTO> NewCourseAsync(string courseCode, string name) => 
             await SeedFetchAndQueueForDeletion(
                 new InsertCourse(courseCode, name), new GetCourseByCourseCode(courseCode), new DeleteCourse(courseCode));

        private async Task<TResponse> SeedFetchAndQueueForDeletion<TResponse>(IDataRequest insertRequest, IDataRequest<TResponse> fetchRequest, IDataRequest deleteRequest)
        {
            await _dataAccess.ExecuteAsync(insertRequest);

            _deleteSeededRecordRequests.Add(deleteRequest);

            return await _dataAccess.FetchAsync(fetchRequest);
        }
    }
}
