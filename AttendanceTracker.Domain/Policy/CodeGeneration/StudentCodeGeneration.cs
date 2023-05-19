using AttendanceTracker.Domain.Constants;

namespace AttendanceTracker.Domain.Policy.CodeGeneration
{
    public static class StudentCodeGeneration
    {
        private static readonly string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static readonly string _numbers = "1234567890";

        static char GetRandomLetter() => _alphabet.ElementAt(Random.Shared.Next(0, _alphabet.Length));

        static char GetRandomNumber() => _numbers.ElementAt(Random.Shared.Next(0, _numbers.Length));

        public static string NewCode()
        {
            var code = new char[StudentCodeConstants.ExpectedLength];

            for (int i = 0; i < code.Length; i++)
            {
                code[i] = i < StudentCodeConstants.LengthOfLeadingLetters ? GetRandomLetter() : GetRandomNumber();
            }

            return new(code);
        }
    }
}
