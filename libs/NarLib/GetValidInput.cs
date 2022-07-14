using System;
using System.Text.RegularExpressions;

namespace NarLib
{
    public static class GetValidInput
    {
        private const string DefaultError = "Valor no valido. Intentelo nuevamente.";

        public static int GetValidIntInput(string prompt, string cursor = "> ", string splitArg = " ",
            bool clear = true, string? parseError = null, Func<int, bool>? conditionFunc = null,
            string? conditionError = null)
        {
            while (true)
            {
                if (clear) { Console.Clear(); }
                string input = GetInput(prompt, cursor).Split(splitArg)[0];
                if (int.TryParse(input, out var res))
                {
                    if (conditionFunc == null) { return res; }
                    if (conditionFunc(res)) { return res; }

                    Console.WriteLine(conditionError ?? DefaultError);
                    Console.ReadLine();
                }
                Console.WriteLine(parseError ?? DefaultError);
                Console.ReadLine();
            }
        }

        public static double GetValidDoubleInput(string prompt, string cursor = "> ", string splitArg = " ",
            bool clear = true, string? parseError = null, Func<double, bool>? conditionFunc = null,
            string? conditionError = null)
        {
            while (true)
            {
                if (clear) { Console.Clear(); }
                string input = GetInput(prompt, cursor).Split(splitArg)[0];
                if (double.TryParse(input, out var res))
                {
                    if (conditionFunc == null) { return res; }
                    if (conditionFunc(res)) { return res; }

                    Console.WriteLine(conditionError ?? DefaultError);
                    Console.ReadLine();
                }
                Console.WriteLine(parseError ?? DefaultError);
                Console.ReadLine();
            }
        }

        public static string GetValidStringInput(string prompt, string cursor = "> ", string splitArg = " ",
            bool clear = true, string? conditionRegex = null, string? conditionError = null, bool ignoreCase = false,
            bool returnMatch = false)
        {
            while (true)
            {
                if (clear) { Console.Clear(); }
                string res = GetInput(prompt, cursor).Split(splitArg)[0];
                if (conditionRegex == null) { return res; }
                Match match = Regex.Match(res, conditionRegex,
                    ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
                if (match.Success) { return returnMatch ? match.ToString() : res; }
                Console.WriteLine(conditionError ?? DefaultError);
                Console.ReadLine();
            }
        }

        public static string GetInput(string? prompt = null, string cursor = "> ")
        {
            if (!string.IsNullOrEmpty(prompt)) { Console.WriteLine(prompt); }
            if (!string.IsNullOrEmpty(cursor)) { Console.Write(cursor); }
            return Console.ReadLine() ?? "";
        }
    }
}
