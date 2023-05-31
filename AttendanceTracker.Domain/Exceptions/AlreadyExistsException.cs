namespace AttendanceTracker.Domain.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException() { }

        public AlreadyExistsException(Type objectAlreadyExisting, params (object Value, string NameOfField)[] conflicts) =>
            Conflicts = conflicts.Select(c => $"{objectAlreadyExisting.Name} already exists with {c.NameOfField}: {c.Value}");

        public readonly IEnumerable<string> Conflicts = Enumerable.Empty<string>();
    }
}
