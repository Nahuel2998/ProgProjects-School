using System;
using System.Text.RegularExpressions;

namespace NarLib
{
    public class GetValidInput
    {
        private static string defaultError = "Valor no valido. Intentelo nuevamente.";

        public static int GetValidIntInput(string prompt, string cursor = "> ", string splitArg = " ", bool clear = true, string parseError = null, Func<int, bool> conditionFunc = null, string conditionError = null)
        {
            int res;
            while (true)
            {
                if (clear) { Console.Clear(); }
                string input = GetInput(prompt, cursor).Split(splitArg)[0];
                if (Int32.TryParse(input, out res))
                {
                    if (conditionFunc != null)
                    {
                        if (conditionFunc(res)) { return res; }

                        Console.WriteLine(String.IsNullOrEmpty(conditionError) ? defaultError : conditionError);
                        Console.ReadLine();
                        continue;
                    }
                    return res;
                }
                Console.WriteLine(String.IsNullOrEmpty(parseError) ? defaultError : parseError);
                Console.ReadLine();
            }
        }

        public static double GetValidDoubleInput(string prompt, string cursor = "> ", string splitArg = " ", bool clear = true, string parseError = null, Func<double, bool> conditionFunc = null, string conditionError = null)
        {
            double res;
            while (true)
            {
                if (clear) { Console.Clear(); }
                string input = GetInput(prompt, cursor).Split(splitArg)[0];
                if (Double.TryParse(input, out res))
                {
                    if (conditionFunc != null)
                    {
                        if (conditionFunc(res)) { return res; }

                        Console.WriteLine(String.IsNullOrEmpty(conditionError) ? defaultError : conditionError);
                        Console.ReadLine();
                        continue;
                    }
                    return res;
                }
                Console.WriteLine(String.IsNullOrEmpty(parseError) ? defaultError : parseError);
                Console.ReadLine();
            }
        }

        public static string GetValidStringInput(string prompt, string cursor = "> ", string splitArg = " ", bool clear = true, string conditionRegex = null, string conditionError = null, bool ignoreCase = false)
        {
            string res;
            while (true)
            {
                if (clear) { Console.Clear(); }
                res = GetInput(prompt, cursor).Split(splitArg)[0];
                if (conditionRegex != null) 
                { 
                    if (Regex.IsMatch(res, conditionRegex, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None)) { return res; }
                    Console.WriteLine(String.IsNullOrEmpty(conditionError) ? defaultError : conditionError);
                    Console.ReadLine();
                    continue;
                }
                return res; 
            }
        }

        public static string GetInput(string prompt = null, string cursor = "> ")
        {
            if (!String.IsNullOrEmpty(prompt)) { Console.WriteLine(prompt); }
            if (!String.IsNullOrEmpty(cursor)) { Console.Write(cursor); }
            return Console.ReadLine();
        }
    }
}
