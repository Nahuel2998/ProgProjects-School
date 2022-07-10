using CommandLine;
using NarExtensions;
using NarLib;

namespace _07151129
{
    internal class Program
    {
        static string[] culpritsList =
            new string[] {
                    "Ushiromiya Kinzo",
                    "Ushiromiya Krauss",
                    "Ushiromiya Natsuhi",
                    "Ushiromiya Jessica",
                    "Ushiromiya Eva",
                    "Ushiromiya Hideyoshi",
                    "Ushiromiya George",
                    "Ushiromiya Rudolf",
                    "Ushiromiya Kyrie",
                    "Ushiromiya Battler",
                    "Ushiromiya Rosa",
                    "Ushiromiya Maria",
                    "Nanjo Terumasa",
                    "Ronoue Genji",
                    "Shannon",
                    "Kanon",
                    "Gohda Toshiro",
                    "Kumasawa Chiyo" };

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
                Console.WriteLine("que no");
                Console.ReadKey();
                return;
            }

            Console.CursorVisible = false;
#if WINFAG
            Console.SetWindowSize(Console.WindowWidth, 40);
#endif

            // Testing console write speed
            //int i = 0;
            //while (true)
            //{
            //    Console.SetCursorPosition(0, 0);
            //    Console.Write(i.ToString().PadCenterBoth(Console.WindowWidth, Console.WindowHeight));
            //    i++;
            //}

            Console.WriteLine(Menu.BuildMenuGetIndex("Who is the culprit?", culpritsList,
                cancellable: false, centered: true, windowWidth: Console.WindowWidth, windowHeight: Console.WindowHeight));
            Console.ReadKey();

#if WINFAG
            // ProgramState.SaveCulpritRegKey("Kanon");
            // ProgramState.Instance.ReadCulpritRegKey();
#endif

            Console.WriteLine(ProgramState.Instance.Options.Culprit?.PadCenterBoth(Console.WindowWidth, Console.WindowHeight) ?? "Hola.");

#if WINFAG
            MessageBox.Show("The code execution cannot proceed because love.dll was not found. Reinstalling the program may fix this problem.", "07151129.exe - System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
        }
    }
}

