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
                        index = index < options.Length - 1 ? ++index : 0;
                        break;
                    case ConsoleKey.UpArrow:
                        index = index > 0 ? --index : options.Length - 1;
                        break;
                    case ConsoleKey.RightArrow: 
                    case ConsoleKey.Enter:
                        options[index].Selected.Invoke();
                        break;
                }
            }
        }

        // Build Menu without Exit Option, returns contained value
        public static dynamic BuildMenuGetSelected(string title, Option[] options)
        {
            int index = 0;

            while (true)
            {
                Console.Clear();
                RenderMenu(title, options, index);

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.DownArrow:
                        index = index < options.Length - 1 ? ++index : 0;
                        break;
                    case ConsoleKey.UpArrow:
                        index = index > 0 ? --index : options.Length - 1;
                        break;
                    case ConsoleKey.RightArrow: 
                    case ConsoleKey.Enter:
                        return options[index].Obj;
                }
            }
        }
        
        // Build Menu without Exit Option, takes strings, returns Index
        public static int BuildMenuGetIndex(string title, string[] options)
        {
            int index = 0;

            while (true)
            {
                Console.Clear();
                RenderMenu(title, options, index);

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.DownArrow:
                        index = index < options.Length - 1 ? ++index : 0;
                        break;
                    case ConsoleKey.UpArrow:
                        index = index > 0 ? --index : options.Length - 1;
                        break;
                    case ConsoleKey.RightArrow: 
                    case ConsoleKey.Enter:
                        return index;
                }
            }
        }
        #endregion

        #region RenderMenu
        // Render Menu with Exit Option
        public static void RenderMenu(string title, Option[] options, int selectedIndex, string exitText)
        {
            Console.WriteLine($"  {title}");
            string separator = "- ".Multiply(title.Length/2 + 3);
            Console.WriteLine(separator);

            for (int i = 0; i < options.Length; i++)
                Console.WriteLine($"{(i == selectedIndex ? "->" : "  ")} {options[i].Name}");
            Console.WriteLine($"{(selectedIndex == options.Length ? "->" : "  ")} {exitText}");

            Console.WriteLine(separator);
        }

        // Render Menu without Exit Option
        public static void RenderMenu(string title, Option[] options, int selectedIndex)
        {
            Console.WriteLine($"  {title}");
            string separator = "- ".Multiply(title.Length/2 + 3);
            Console.WriteLine(separator);

            for (int i = 0; i < options.Length; i++)
                Console.WriteLine($"{(i == selectedIndex ? "->" : "  ")} {options[i].Name}");

            Console.WriteLine(separator);
        }

        // Render Menu without Exit Option, takes string inputs
        public static void RenderMenu(string title, string[] options, int selectedIndex)
        {
            Console.WriteLine($"  {title}");
            string separator = "- ".Multiply(title.Length/2 + 3);
            Console.WriteLine(separator);

            for (int i = 0; i < options.Length; i++)
                Console.WriteLine($"{(i == selectedIndex ? "->" : "  ")} {options[i]}");

            Console.WriteLine(separator);
        }
        #endregion
    }

    public class Option
    {
        public string Name { get; }
        public Action Selected { get; }
        public dynamic Obj { get; }
        public ConsoleKeyInfo Shortcut { get; }
    
        public Option(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }

        public Option(string name, dynamic obj)
        {
            Name = name;
            Obj = obj;
        }
    }
}
