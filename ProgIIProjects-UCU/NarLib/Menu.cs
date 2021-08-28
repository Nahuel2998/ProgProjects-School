using System;
using System.Collections.Generic;

static void BuildMenu(List<Option> options)
{
    ConsoleKeyInfo key;
    int index = 0;

    while (true)
    {
        switch (Console.ReadKey())
        {
            case Consolekey.DownArrow:
                if (index + 1 < options.Count)
                    RenderMenu(options, options[++index])
                break;
            case Consolekey.UpArrow:
                if (index > 0)
                    RenderMenu(options, options[--index])
                break;
            case ConsoleKey.Enter:
                options[index].Selected.Invoke();
                index = 0;
                break;
        }
    }
}

static void RenderMenu(List<Option> options, Option selectedOption)
{
    Console.Clear();

    foreach (o in options)
        Console.WriteLine($"{o == selectedOption ? '>' : ''} {option.Name}");
}

class Option
{
    public string Name { get; }
    public Action Selected { get; }
    public ConsoleKeyInfo Shortcut { get; }

    public Option(string name, Action selected, ConsoleKeyInfo shortcut = null)
    {
        Name = name;
        Selected = selected; 
        Shortcut = shortcut;
    }
}
