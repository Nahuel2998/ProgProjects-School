using NarExtensions;
using System.Text;

namespace NarLib
{
    public static class Menu
    {
        #region BuildMenu
        // Build Menu Generic
        // Main Build Menu
        private static object? BuildMenuFunc(string title, IReadOnlyList<object> options,
            Func<IReadOnlyList<object>, int, object?> action, /* out dynamic result */ string? exitText = null,
            string? bottomText = null, bool cancellable = true, bool closeAfter = false, string[]? stringOptions = null,
            Func<string>? bottomTextFunc = null, bool centered = false, int? windowWidth = null, int? windowHeight = null, string? separator = null)
        {
            int index = 0;
            int maxVal = options.Count;
            bool isThereAnExitOption = true;
            if (exitText == null)
            {
                maxVal--;
                isThereAnExitOption = false;
            }

            stringOptions ??= (string[]) options;
            
            int width = windowWidth ?? Console.WindowWidth;
            int height = windowHeight ?? Console.WindowHeight;

            // Needed when text is gonna be centered (since whitespace doesn't override other chars.)
            Console.Clear();

            while (true)
            {
                Console.SetCursorPosition(0, 0);

                if (!centered)
                { RenderMenu(title, stringOptions, index, exitText, bottomText, bottomTextFunc, separator); }
                else
                { RenderMenuCentered(title, stringOptions, index, width, height, exitText, bottomText, bottomTextFunc, separator); }

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
                        object? res = action.Invoke(options, index);
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
        public static void BuildMenu(string title, Option[] options, string? exitText = null, string? bottomText = null,
            bool cancellable = true, bool closeAfter = false, string[]? stringOptions = null,
            Func<string>? bottomTextFunc = null, bool centered = false, int windowWidth = 0, int windowHeight = 0, string? separator = null)
        {
            static object? InvokeOption(IReadOnlyList<object> xOptions, int xIndex)
            {
                ((Option) xOptions[xIndex]).Selected?.Invoke();
                return null;
            }

            BuildMenuFunc(title, options, InvokeOption, exitText, bottomText, cancellable, closeAfter,
                stringOptions ?? Option.GetNamesFromOptionList(options), bottomTextFunc, centered, windowWidth, windowHeight, separator);
        }
        
        // Build Menu with (or without) Exit Option, returns contained value, null if cancelled
        public static object? BuildMenuGetSelected(string title, Option[] options, string? exitText = null,
            string? bottomText = null, bool cancellable = true, bool closeAfter = true, string[]? stringOptions = null,
            Func<string>? bottomTextFunc = null, bool centered = false, int windowWidth = 0, int windowHeight = 0, string? separator = null)
        {
            static object? GetOption(IReadOnlyList<object> xOptions, int xIndex)
            { return ((Option) xOptions[xIndex]).Obj; }

            return BuildMenuFunc(title, options, GetOption, exitText, bottomText, cancellable, closeAfter,
                stringOptions ?? Option.GetNamesFromOptionList(options), bottomTextFunc, centered, windowWidth, windowHeight, separator);
        }
        
        // Build Menu with (or without) Exit Option, takes strings, returns Index, -1 if cancelled
        public static int BuildMenuGetIndex(string title, string[] options, string? exitText = null,
            string? bottomText = null, bool cancellable = true, bool closeAfter = true, bool centered = false, 
            int windowWidth = 0, int windowHeight = 0, string? separator = null)
        {
            static object GetIndex(IReadOnlyList<object> xOptions, int xIndex)
            { return xOptions.Count != 0 ? xIndex : -1; }

            return (int) (BuildMenuFunc(title, options, GetIndex, exitText, bottomText, cancellable, closeAfter,
                centered:centered, windowWidth:windowWidth, windowHeight:windowHeight, separator: separator) ?? -1);
        }
        #endregion

        #region RenderMenu
        // Render Menu with (or without) Exit Option
        public static void RenderMenu(string title, IEnumerable<Option> options, int selectedIndex,
            string? exitText = null, string? bottomText = null, Func<string>? bottomTextFunc = null, string? separator = null)
        { RenderMenu(title, Option.GetNamesFromOptionList(options), selectedIndex, exitText, bottomText, bottomTextFunc, separator); }

        // Render Menu with (or without) Exit Option, takes string inputs
        // Main Render Menu method
        public static void RenderMenu(string title, string[] options, int selectedIndex, string? exitText = null,
            string? bottomText = null, Func<string>? bottomTextFunc = null, string? separator = null)
        {
            StringBuilder res = new();

            res.AppendLine($"  {title}");
            separator ??= "- ".Multiply(title.Length/2 + 3);
            if (separator.Length > 0)
            { res.AppendLine(separator); }

            for (int i = 0; i < options.Length; i++)
            { res.AppendLine($"{(i == selectedIndex ? "->" : "  ")} {options[i]}"); }
            if (exitText != null)
            { res.AppendLine($"{(selectedIndex == options.Length ? "->" : "  ")} {exitText}"); }

            if (separator.Length > 0)
            { res.AppendLine(separator); }
            res.AppendLine(bottomText ?? bottomTextFunc?.Invoke());

            Console.Write(res.ToString());
        }

        public static void RenderMenuCentered(string title, string[] options, int selectedIndex, int windowWidth, int windowHeight,
            string? exitText = null, string? bottomText = null, Func<string>? bottomTextFunc = null, string? separator = null)
        {
            StringBuilder res = new();

            res.AppendLine(title.PadCenterHorizontal(windowWidth));
            separator ??= (" " + "- ".Multiply(title.Split('\n').Select(x => x.Length).Max() / 2 + 3));
            if (separator.Length > 0)
            {
                separator = separator.PadCenterHorizontal(windowWidth);
                res.AppendLine(separator);
            }

            for (int i = 0; i < options.Length; i++)
            {
                bool isSelected = i == selectedIndex;
                res.AppendLine($"{(isSelected ? "---[" : "  ")} {options[i]} {(isSelected ? "]---" : "  ")}".PadCenterHorizontal(windowWidth));
            }
            if (exitText != null)
            { res.AppendLine($"{(selectedIndex == options.Length ? "---[" : "  ")} {exitText} {(selectedIndex == options.Length ? "]---" : "  ")}".PadCenterHorizontal(windowWidth)); }

            if (separator.Length > 0)
            { res.AppendLine(separator); }
            res.AppendLine((bottomText ?? bottomTextFunc?.Invoke())?.PadCenterHorizontal(windowWidth));

            Console.Write(res.ToString().PadCenterVertical(windowHeight));
        }
        #endregion
    }

    public class Option
    {
        public string Name { get; }
        public Action? Selected { get; }
        public object? Obj { get; }
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
