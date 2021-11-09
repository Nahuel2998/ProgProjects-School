using System.Text;

namespace NarExtensions
{
    public static class StringExtensions
    {
        public static string Multiply(this string source, int multiplier)
        {
            StringBuilder res = new StringBuilder(multiplier * source.Length);
            for (int i = 0; i < multiplier; i++) { res.Append(source); }

            return res.ToString();
        }
    }
}
