using AttendanceTracker.Domain.Exceptions;
using AttendanceTracker.Domain.Extensions;
using AttendanceTracker.Domain.Policy.Validation;

namespace AttendanceTracker.Data.DataRequestObjects.LogRequests
{
    public class GetResponseTimesByRequestPath : Parameterless_DataRequest<ResponseTimeLog_DTO>
    {
        public GetResponseTimesByRequestPath(string requestPathToLookFor) : this(new List<string>() { requestPathToLookFor }) { }

        public GetResponseTimesByRequestPath(List<string> requestPathsToLookFor) => RequestPathsToLookFor = requestPathsToLookFor;

        public List<string> RequestPathsToLookFor { get; set; } = new List<string>();


        private string GetPathsForQuery()
        {
            if (!RequestPathsToLookFor.Any())
            {
                throw new ValidationFailedException(ValidationFailureMessage.MissingRequiredField(nameof(RequestPathsToLookFor)));
            }

            var paths = RequestPathsToLookFor.WrapValuesWithApostrophes();

            return RequestPathsToLookFor.Count > 1 ? paths.AggregateWithCommas() : paths.First();
        }

        public override string GetSql() => Select.FromTable(TableNames.ResponseTimeLog, where: $"RequestPath IN ( {GetPathsForQuery()} )");
    }
}
