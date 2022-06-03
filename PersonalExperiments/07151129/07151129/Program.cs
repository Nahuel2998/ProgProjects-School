using CommandLine;
using NarExtensions;

namespace _07151129
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new(with =>
            {
                with.IgnoreUnknownArguments = true;
                with.AutoHelp = false;
                with.CaseSensitive = false;
            });

            ProgramState.Instance.Options = parser.ParseArguments<Options>(args).Value;

            if (ProgramState.Instance.Options.Help)
            {
                Console.WriteLine(string.Join(' ', args));
                return;
            }

            if (!ProgramState.Instance.Options.Please)
            {
                Console.WriteLine("no");
                Console.ReadKey();
                return;
            }

            Console.SetWindowSize(Console.WindowWidth, 40);
            Console.WriteLine(ProgramState.Instance.Options.Culprit?.PadBoth(Console.WindowWidth / 2).PadVertical(40) ?? "Hola.");

            MessageBox.Show("The code execution cannot proceed because love.dll was not found. Reinstalling the program may fix this problem.", "07151129.exe - System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

