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

        public static string PadCenterHorizontal(this string source, int totalWidth, char paddingChar = ' ')
        { return source.PadLeft((totalWidth - source.Length) / 2 + source.Length, paddingChar).PadRight(totalWidth, paddingChar); }

        public static string PadCenterVertical(this string source, int totalHeight)
        {
            int padHeight = (totalHeight - Regex.Matches(source, @"$", RegexOptions.Multiline).Count);
            int halfPadHeight = padHeight / 2;

            StringBuilder res = new("\n".Multiply(halfPadHeight + padHeight % 2));
            res.Append(source);
            res.Append("\n".Multiply(halfPadHeight));

            return res.ToString();
        }

        public static string PadCenterBoth(this string source, int totalWidth, int totalHeight)
        { return source.PadCenterHorizontal(totalWidth).PadCenterVertical(totalHeight); }
    }
}

