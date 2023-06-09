﻿using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;
using AttendanceTracker.Domain.Constants;
using AttendanceTracker.Domain.Extensions;
using System.Data.SqlClient;

namespace AttendanceTracker.Data.Tests.DataRequestTests.SubjectTests
{
    public class InsertSubjectTests : DataTest
    {
        [Fact]
        public async Task InsertSubject_Given_SubjectCode_IsNull_ShouldThrow_SqlException()
        {
            var insertRequest = new InsertSubject(subjectCode: null!, RandomString());

            await Assert.ThrowsAsync<SqlException>(async () => await _dataAccess.ExecuteAsync(insertRequest));
        }

        [Fact]
        public async Task InsertSubject_Given_SubjectCode_AlreadyExists_ShouldReturn_NoRowsUpdated()
        {
            var existingSubject = await SeedAsync(new SeedSubjectRequest());

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertSubject(existingSubject.SubjectCode, RandomString(SubjectCodeConstants.MaxLength)));

            Assert.True(rowsAffected.NoRowsAreUpdated());
        }

        [Fact]
        public async Task InsertSubject_Given_SubjectIsInserted_ShouldReturn_RowsUpdated()
        {
            var subjectCode = RandomString(SubjectCodeConstants.MaxLength);

            var result = await _dataAccess.ExecuteAsync(new InsertSubject(subjectCode, RandomString()));

            await _dataAccess.ExecuteAsync(new DeleteSubject(subjectCode));

            Assert.True(result.AnyRowsAreUpdated());
        }
    }
}
