using System;

namespace NarLib
{
    public class Menu
    {
        #region BuildMenu
        // Build Menu with Exit Option
        static void BuildMenu(string title, Option[] options, string exitText)
        {
            int index = 0;
            int exitVal = options.Length;

            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.DownArrow:
                        RenderMenu(options, index + 1 < exitVal ? ++index : exitVal, exitText);
                        break;
                    case ConsoleKey.UpArrow:
                        RenderMenu(options, index > 0 ? --index : exitVal, exitText);
                        break;
                    case ConsoleKey.RightArrow: 
                    case ConsoleKey.Enter:
                        if (index == exitVal)
                            return;
                        options[index].Selected.Invoke();
                        RenderMenu(options, index, exitText);
                        break;
                    case ConsoleKey.LeftArrow:
                        return;
                }
            }
        }

        // Build Menu without Exit Option
        static void BuildMenu(string title, Option[] options)
        {
            int index = 0;

            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.DownArrow:
                        RenderMenu(options, index + 1 < options.Length ? ++index : options.Length);
                        break;
                    case ConsoleKey.UpArrow:
                        RenderMenu(options, index > 0 ? --index : options.Length);
                        break;
                    case ConsoleKey.RightArrow: 
                    case ConsoleKey.Enter:
                        options[index].Selected.Invoke();
                        RenderMenu(options, index);
                        break;
                }
            }
        }
        #endregion

        #region RenderMenu
        // Render Menu with Exit Option
        static void RenderMenu(Option[] options, int selectedIndex, string exitText)
        {
            Console.Clear();
            for (int i = 0; i < options.Length; i++)
                Console.WriteLine($"{(i == selectedIndex ? "->" : "")}\t{options[i].Name}");
            Console.WriteLine($"{(selectedIndex == options.Length ? "->" : "")}\t{exitText}");
        }

        // Render Menu without Exit Option
        static void RenderMenu(Option[] options, int selectedIndex)
        {
            Console.Clear();
            for (int i = 0; i < options.Length; i++)
                Console.WriteLine($"{(i == selectedIndex ? "->" : "")}\t{options[i].Name}");
        }
        #endregion
    }

    class Option
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
