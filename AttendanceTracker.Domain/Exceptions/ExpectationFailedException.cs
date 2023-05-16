namespace AttendanceTracker.Domain.Exceptions
{
    public class ExpectationFailedException : Exception
    {
        public ExpectationFailedException(string nameOfRequestFailing) : base($"{nameOfRequestFailing} failed unexpectedly.") { }

        public ExpectationFailedException(string nameOfRequestFailing, string extraInfo) : base($"{nameOfRequestFailing} failed unexpectedly. {extraInfo}") { }
    }
}
