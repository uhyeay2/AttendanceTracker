namespace AttendanceTracker.Domain.Policy.Validation
{
    public static class ValidationFailureMessage
    {
        public static string MissingRequiredField(string nameOfRequiredField) => $"{nameOfRequiredField} is a required field!";

        public static string MissingOneOfAnyRequiredFields(IEnumerable<string> namesOfRequiredField) => 
            $"Must provide at least one of the following fields: {namesOfRequiredField.Aggregate((a, b)=> $"{a}, {b}")}";
    }
}
