using AttendanceTracker.Data.Abstraction.Interfaces;

namespace AttendanceTracker.Tests.Shared.DataSeeder
{
    public class DataSeeder
    {
        public DataSeeder(IDataAccess dataAccess) => _dataAccess = dataAccess;

        private readonly IDataAccess _dataAccess;

        private readonly List<IDataRequest> _deleteSeededRecordRequests = new();

        public async Task<TResponse> SeedFetchAndQueueForDeletionAsync<TResponse>(IDataRequest insertRequest, IDataRequest<TResponse> fetchRequest, IDataRequest deleteRequest)
        {
            await _dataAccess.ExecuteAsync(insertRequest);

            _deleteSeededRecordRequests.Add(deleteRequest);

            return await _dataAccess.FetchAsync(fetchRequest);
        }

        public async Task PurgeSeededRecordsAsync()
        {
            const int maxCyclesToAttemptDeletingRecords = 5;
            var currentCycle = 0;

            // loop through a limited number of times or until all deletes are successfully executed/removed from list
            while (currentCycle < maxCyclesToAttemptDeletingRecords && _deleteSeededRecordRequests.Any())
            {
                // Loop backwards through requests deleting seeded records. Backwards to try to avoid Foreign Key Conflicts
                for (int i = _deleteSeededRecordRequests.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        // attempt to delete record, remove from list if successful
                        await _dataAccess.ExecuteAsync(_deleteSeededRecordRequests[i]);
                        _deleteSeededRecordRequests.RemoveAt(i);
                    }
                    catch (Exception) { /* TODO: Log Purge Failures ? */ }
                }
            }
        }
    }
}
