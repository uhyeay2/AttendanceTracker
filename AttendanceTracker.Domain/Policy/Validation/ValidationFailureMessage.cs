namespace AttendanceTracker.Domain.Policy.Validation
{
    public static class ValidationFailureMessage
    {
        public static string MustBeLettersOnly(string value, string nameOfField) =>
            $"{nameOfField} Contains invalid characters. Must be Letters Only. Value Received: {value}";

        public static string MustMeetMinimumLengthRequirement(string nameOfField, int minimumLength, int actualLength) => 
            $"{nameOfField} Does not meet Minimum Length Requirement: {minimumLength}. Length Received: {actualLength}";

        public static string MustMeetMaximumLengthRequirement(string nameOfField, int maximumLength, int actualLength) => 
            $"{nameOfField} Does not meet Maximum Length Requirement: {maximumLength}. Length Received: {actualLength}";

        public static string MissingRequiredField(string nameOfRequiredField) => 
            $"{nameOfRequiredField} is a required field!";

        public static string MissingOneOfAnyRequiredFields(IEnumerable<string> namesOfRequiredField) => 
            $"Must provide at least one of the following fields: {namesOfRequiredField.Aggregate((a, b)=> $"{a}, {b}")}";
    }
}
