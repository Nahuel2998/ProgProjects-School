using System;
using NarExtensions;

namespace NarLib
{
    public static class Menu
    {
        #region BuildMenu
        // Build Menu with Exit Option
        public static void BuildMenu(string title, Option[] options, string exitText)
        {
            int index = 0;
            int exitVal = options.Length;

            while (true)
            {
                Console.Clear();
                RenderMenu(title, options, index, exitText);

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.DownArrow:
                        index = index < options.Length ? ++index : 0;
                        break;
                    case ConsoleKey.UpArrow:
                        index = index > 0 ? --index : options.Length;
                        break;
                    case ConsoleKey.RightArrow: 
                    case ConsoleKey.Enter:
                        if (index == exitVal)
                            return;
                        options[index].Selected.Invoke();
                        break;
                    case ConsoleKey.LeftArrow:
                        return;
                }
            }
        }

        // Build Menu without Exit Option
        public static void BuildMenu(string title, Option[] options)
        {
            int index = 0;

            while (true)
            {
                Console.Clear();
                RenderMenu(title, options, index);

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.DownArrow:
                        index = index < options.Length ? ++index : 0;
                        break;
                    case ConsoleKey.UpArrow:
                        index = index > 0 ? --index : options.Length;
                        break;
                    case ConsoleKey.RightArrow: 
                    case ConsoleKey.Enter:
                        options[index].Selected.Invoke();
                        break;
                }
            }
        }
        #endregion

        #region RenderMenu
        // Render Menu with Exit Option
        public static void RenderMenu(string title, Option[] options, int selectedIndex, string exitText)
        {
            Console.WriteLine($"  [{title}]");
            string separator = "- ".Multiply(title.Length/2 + 4);
            Console.WriteLine(separator);

            for (int i = 0; i < options.Length; i++)
                Console.WriteLine($"{(i == selectedIndex ? "->" : "  ")} {options[i].Name}");
            Console.WriteLine($"{(selectedIndex == options.Length ? "->" : "  ")} {exitText}");

            Console.WriteLine(separator);
        }

        // Render Menu without Exit Option
        public static void RenderMenu(string title, Option[] options, int selectedIndex)
        {
            Console.WriteLine($"  [{title}]");
            string separator = "- ".Multiply(title.Length/2 + 4);
            Console.WriteLine(separator);

            for (int i = 0; i < options.Length; i++)
                Console.WriteLine($"{(i == selectedIndex ? "->" : "  ")} {options[i].Name}");

            Console.WriteLine(separator);
        }
        #endregion
    }

    public class Option
    {
        public string Name { get; }
        public Action Selected { get; }
        public ConsoleKeyInfo Shortcut { get; }
    
        public Option(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }
    }
}
