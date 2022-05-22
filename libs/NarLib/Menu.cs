using System;
using System.Collections.Generic;
using System.Linq;
using NarExtensions;

namespace NarLib
{
    public static class Menu
    {
        #region BuildMenu
        // Build Menu Generic
        // Main Build Menu
        private static object BuildMenuFunc(string title, IReadOnlyList<object> options,
            Func<IReadOnlyList<object>, int, object> action, /* out dynamic result */ string exitText = null,
            string bottomText = null, bool cancellable = true, bool closeAfter = false, string[] stringOptions = null,
            Func<string> bottomTextFunc = null)
        {
            int index = 0;
            int maxVal = options.Count;
            bool isThereAnExitOption = true;
            if (exitText == null)
            {
                maxVal--;
                isThereAnExitOption = false;
            }

            stringOptions = (stringOptions ?? options) as string[];

            while (true)
            {
                Console.Clear();
                RenderMenu(title, stringOptions, index, exitText, bottomText, bottomTextFunc);

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.DownArrow:
                        index = index < maxVal ? ++index : 0;
                        break;
                    case ConsoleKey.UpArrow:
                        index = index > 0 ? --index : maxVal;
                        break;
                    case ConsoleKey.RightArrow: 
                    case ConsoleKey.Enter:
                        if (isThereAnExitOption && index == maxVal)
                            return null;
                        // result = action(options, index);
                        var res = action(options, index);
                        if (closeAfter)
                            return res;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (cancellable)
                            return null;
                        break;
                }
            }
        }
        
        // Build Menu with (or without) Exit Option
        public static void BuildMenu(string title, Option[] options, string exitText = null, string bottomText = null,
            bool cancellable = true, bool closeAfter = false, string[] stringOptions = null,
            Func<string> bottomTextFunc = null)
        {
            static object InvokeOption(IReadOnlyList<object> xOptions, int xIndex)
            {
                ((Option) xOptions[xIndex]).Selected.Invoke();
                return null;
            }

            BuildMenuFunc(title, options, InvokeOption, exitText, bottomText, cancellable, closeAfter,
                stringOptions ?? Option.GetNamesFromOptionList(options), bottomTextFunc);
        }
        
        // Build Menu with (or without) Exit Option, returns contained value, null if cancelled
        public static object BuildMenuGetSelected(string title, Option[] options, string exitText = null,
            string bottomText = null, bool cancellable = true, bool closeAfter = true, string[] stringOptions = null,
            Func<string> bottomTextFunc = null)
        {
            static object GetOption(IReadOnlyList<object> xOptions, int xIndex)
            { return ((Option) xOptions[xIndex]).Obj; }

            return BuildMenuFunc(title, options, GetOption, exitText, bottomText, cancellable, closeAfter,
                stringOptions ?? Option.GetNamesFromOptionList(options), bottomTextFunc);
        }
        
        // Build Menu with (or without) Exit Option, takes strings, returns Index, -1 if cancelled
        public static int BuildMenuGetIndex(string title, string[] options, string exitText = null,
            string bottomText = null, bool cancellable = true, bool closeAfter = true)
        {
            static object GetIndex(IReadOnlyList<object> xOptions, int xIndex)
            { return xOptions.Count != 0 ? xIndex : -1; }

            return (int) (BuildMenuFunc(title, options, GetIndex, exitText, bottomText, cancellable, closeAfter) ?? -1);
        }
        #endregion

        #region RenderMenu
        // Render Menu with (or without) Exit Option
        public static void RenderMenu(string title, IEnumerable<Option> options, int selectedIndex,
            string exitText = null, string bottomText = null, Func<string> bottomTextFunc = null)
        { RenderMenu(title, Option.GetNamesFromOptionList(options), selectedIndex, exitText, bottomText, bottomTextFunc); }

        // Render Menu with (or without) Exit Option, takes string inputs
        // Main Render Menu method
        public static void RenderMenu(string title, string[] options, int selectedIndex, string exitText = null,
            string bottomText = null, Func<string> bottomTextFunc = null)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            Console.WriteLine($"  {title}");
            string separator = "- ".Multiply(title.Length/2 + 3);
            Console.WriteLine(separator);

            for (int i = 0; i < options.Length; i++)
                Console.WriteLine($"{(i == selectedIndex ? "->" : "  ")} {options[i]}");
            if (exitText != null)
                Console.WriteLine($"{(selectedIndex == options.Length ? "->" : "  ")} {exitText}");

            Console.WriteLine(separator);
            Console.WriteLine(bottomText ?? bottomTextFunc?.Invoke());
        }
        #endregion
    }

    public class Option
    {
        public string Name { get; }
        public Action Selected { get; }
        public object Obj { get; }
        // public ConsoleKeyInfo Shortcut { get; }
    
        public Option(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }

        public Option(string name, object obj)
        {
            Name = name;
            Obj = obj;
        }

        public static string[] GetNamesFromOptionList(IEnumerable<Option> options) =>
            options.Select(option => option.Name).ToArray();
    }
}
