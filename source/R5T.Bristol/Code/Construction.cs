using System;
using System.Text.RegularExpressions;


namespace R5T.Bristol
{
    public static class Construction
    {
        public static void SubMain()
        {
            Construction.TestRegex();
        }

        public static void TestContainerIdentificationCheckDigit()
        {

        }

        public static void TestRegex()
        {
            var testStrings = new string[]
            {
                "abc",
                "123",
                "XYz",
                "ABC",
                "TUV",
                "WYX"
            };

            var pattern = @"[A-Z]{3}";

            var regex = new Regex(pattern);
            var writer = Console.Out;
            foreach (var testString in testStrings)
            {
                var isMatch = regex.IsMatch(testString);
                writer.WriteLine($"Regex {pattern} - {testString}, is match: {isMatch}");
            }
        }
    }
}
