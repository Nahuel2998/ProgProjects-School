using System;
using System.Text.RegularExpressions;

namespace NarLib
{
    public class GetValidInput
    {
        private const string DefaultError = "Valor no valido. Intentelo nuevamente.";

        public static int GetValidIntInput(string prompt, string cursor = "> ", string splitArg = " ",
            bool clear = true, string parseError = null, Func<int, bool> conditionFunc = null,
            string conditionError = null)
        {
            while (true)
            {
                if (clear) { Console.Clear(); }
                string input = GetInput(prompt, cursor).Split(splitArg)[0];
                if (int.TryParse(input, out var res))
                {
                    if (conditionFunc == null) { return res; }
                    if (conditionFunc(res)) { return res; }

                    Console.WriteLine(string.IsNullOrEmpty(conditionError) ? DefaultError : conditionError);
                    Console.ReadLine();
                }
                Console.WriteLine(string.IsNullOrEmpty(parseError) ? DefaultError : parseError);
                Console.ReadLine();
            }
        }

        public static double GetValidDoubleInput(string prompt, string cursor = "> ", string splitArg = " ",
            bool clear = true, string parseError = null, Func<double, bool> conditionFunc = null,
            string conditionError = null)
        {
            while (true)
            {
                if (clear) { Console.Clear(); }
                string input = GetInput(prompt, cursor).Split(splitArg)[0];
                if (double.TryParse(input, out var res))
                {
                    if (conditionFunc == null) { return res; }
                    if (conditionFunc(res)) { return res; }

                    Console.WriteLine(string.IsNullOrEmpty(conditionError) ? DefaultError : conditionError);
                    Console.ReadLine();
                }
                Console.WriteLine(string.IsNullOrEmpty(parseError) ? DefaultError : parseError);
                Console.ReadLine();
            }
        }

        public static string GetValidStringInput(string prompt, string cursor = "> ", string splitArg = " ",
            bool clear = true, string conditionRegex = null, string conditionError = null, bool ignoreCase = false)
        {
            while (true)
            {
                if (clear) { Console.Clear(); }
                string res = GetInput(prompt, cursor).Split(splitArg)[0];
                if (conditionRegex == null) { return res; } 
                if (Regex.IsMatch(res, conditionRegex, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None)) { return res; }
                Console.WriteLine(string.IsNullOrEmpty(conditionError) ? DefaultError : conditionError);
                Console.ReadLine();
            }
        }

        public static string GetInput(string prompt = null, string cursor = "> ")
        {
            if (!string.IsNullOrEmpty(prompt)) { Console.WriteLine(prompt); }
            if (!string.IsNullOrEmpty(cursor)) { Console.Write(cursor); }
            return Console.ReadLine();
        }
    }
}
