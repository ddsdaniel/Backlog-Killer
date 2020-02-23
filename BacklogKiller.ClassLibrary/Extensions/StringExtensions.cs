using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BacklogKiller.ClassLibrary.Extensions
{
    public static class StringExtensions
    {
        public static string[] SplitCamelCase(this string input)
        {
            var words = Regex.Matches(input, "(^[a-z]+|[A-Z]+(?![a-z])|[A-Z][a-z]+)")
                .OfType<Match>()
                .Select(m => m.Value)
                .ToArray();

            return words;
        }

        public static bool IsCamelCase(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return true;

            //only letters and numbers
            if (!Regex.IsMatch(input, @"^[a-zA-Z0-9]+$"))
                return false;

            //the first character must be a letter
            if (!Regex.IsMatch(input[0].ToString(), @"^[a-zA-Z]+$"))
                return false;

            //the first character must be uppercase
            if (input[0].ToString() != input[0].ToString().ToUpper())
                return false;

            return true;
        }
    }
}
