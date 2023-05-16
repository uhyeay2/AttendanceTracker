namespace AttendanceTracker.Domain.Exceptions
{
    public class DoesNotExistException : Exception
    {
        public DoesNotExistException(Type typeNotExisting, object value, string nameOfField) : this(typeNotExisting, (value, nameOfField)) { }

        public DoesNotExistException(Type typeNotExisting, params (object? Value, string NameOfField)[] valuesSearchedBy) =>
            ValuesSearchedBy = valuesSearchedBy.Select(_ => $"{typeNotExisting.Name} not found with {_.NameOfField}: {_.Value}");

        public readonly IEnumerable<string> ValuesSearchedBy;
    }
}
