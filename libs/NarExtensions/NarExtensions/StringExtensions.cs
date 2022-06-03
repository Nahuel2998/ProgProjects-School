using System.Text;
using System.Text.RegularExpressions;

namespace NarExtensions
{
    public static class StringExtensions
    {
        public static string Multiply(this string source, int multiplier)
        {
            StringBuilder res = new(multiplier * source.Length);
            for (int i = 0; i < multiplier; i++) { res.Append(source); }

            return res.ToString();
        }

        public static string PadBoth(this string source, int totalWidth, char paddingChar = ' ')
        { return source.PadLeft(totalWidth - source.Length / 2 + source.Length, paddingChar).PadRight(totalWidth, paddingChar); }

        public static string PadVertical(this string source, int totalHeight)
        {
            int padHeight = (totalHeight - Regex.Matches(source, @"$", RegexOptions.Multiline).Count);
            int halfPadHeight = padHeight / 2;

            StringBuilder res = new("\n".Multiply(halfPadHeight));
            res.Append(source);
            res.Append("\n".Multiply(halfPadHeight + padHeight % 2));

            return res.ToString();
        }
    }
}
