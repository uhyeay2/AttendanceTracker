namespace AttendanceTracker.Domain.Policy.CodeGeneration
{
    public static class StudentCodeGeneration
    {
        private static readonly char[] _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();

        private static char GetNextCharacter() => _alphabet.ElementAt(Random.Shared.Next(0, _alphabet.Length));

        public static string NewCode()
        {
            var threeRandomLetters = $"{GetNextCharacter()}{GetNextCharacter()}{GetNextCharacter()}";

            var fourDigitNumber = Random.Shared.Next(1000, 9999).ToString();

            return threeRandomLetters + fourDigitNumber;
        }
    }
}
