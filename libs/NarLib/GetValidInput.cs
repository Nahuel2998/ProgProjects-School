using System;

namespace NarLib
{
    public class GetValidInput
    {
        private static string defaultError = "Valor no valido. Intentelo nuevamente.";

        public static int GetValidIntInput(string prompt, string splitArg = " ", bool clear = true, string parseError = null, Func<int, bool> conditionFunc = null, string conditionError = null)
        {
            while (true)
            {
                int res;
                if (clear) { Console.Clear(); }
                Console.WriteLine(prompt);
                Console.Write("> ");
                string input = Console.ReadLine().Split(splitArg)[0];
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

        public static double GetValidDoubleInput(string prompt, string splitArg = " ", bool clear = true, string parseError = null, Func<double, bool> conditionFunc = null, string conditionError = null)
        {
            while (true)
            {
                double res;
                if (clear) { Console.Clear(); }
                Console.WriteLine(prompt);
                Console.Write("> ");
                string input = Console.ReadLine().Split(splitArg)[0];
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
    }
}
