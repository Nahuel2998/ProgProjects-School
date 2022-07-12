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
                Console.WriteLine("rude");
                Console.ReadKey();
                return;
            }

            Console.CursorVisible = false;
#if WINFAG
            Console.SetWindowSize(Console.WindowWidth, 40);
#endif
            AddQuestion(new Question("What's the name of the second mansion?",
            "Rokkenjima : Kuwadorian : Kumasawa : There's only one mansion",
            1));

            // Hardcoded Maria question
            // TODO: Change this to something else.
            Menu.BuildMenuGetIndex("What does Maria love to say?".PadCenterHorizontal(Console.WindowWidth) + "\n"
                    + "- - - - - - - - - - - - - - - - -".PadCenterHorizontal(Console.WindowWidth),
                    "uooooooooh!:uu-uu!:auau!:- - - - - - - - - - - - - - - - -: ".Split(':'),
                cancellable: false, centered: true, windowWidth: Console.WindowWidth, windowHeight: Console.WindowHeight,
                separator: "");

            AddQuestion(new Question("Who's the only human (invited by Kinzo)\nthat lives past October 6th 1986 across all episodes?",
            "Ushiromiya Battler : Ushiromiya Eva : Ushiromiya Ange : None",
            3,
            () =>
            {
                Console.Clear();
                Console.WriteLine("y si".PadCenterBoth());
                Console.ReadKey();
            }));

            AddQuestion(new Question("Who's the only piece (invited to the conference)\nthat lives past October 6th 1986 across all episodes?",
            "Ushiromiya Battler : Ushiromiya Eva : Ushiromiya Ange : On the 9th Twilight, none shall be left alive",
            2,
            () =>
            {
                Console.Clear();
                Console.WriteLine("\n".Multiply(Console.WindowHeight / 2 - 2));
                Console.WriteLine("That's right!".PadCenterHorizontal(Console.WindowWidth));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kinzo is dead at the starting time for all games.".PadCenterHorizontal(Console.WindowWidth));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("So he didn't invite a single human.".PadCenterHorizontal(Console.WindowWidth));
                Console.ReadKey();
            }));

            AddQuestion(new Question("Who's the only human (who attends the conference)\nthat lives past October 6th 1986 across all episodes?",
            "Ushiromiya Battler : Ushiromiya Eva : Ushiromiya Ange : On the 9th Twilight, none shall be left alive I said",
            1,
            () =>
            {
                Console.Clear();
                Console.WriteLine("\n".Multiply(Console.WindowHeight / 2 - 2));
                Console.WriteLine("g\nEven if Ange didn't attend the conference, she was invited.\nShe was just sick that day.".PadCenterBoth(Console.WindowWidth, Console.WindowHeight));
                Console.ReadKey();
            }));

            // Amount of people on the island question
            AddQuestion(new Question("How many humans exist on the island?",
            ProgramState.Instance.Options.Nome
            ? "At least 20 : Exactly 19 : Exactly 18 : No more than 17"
            : "At least 19 : Exactly 18 : Exactly 17 : No more than 16",
            3,
            () =>
            {
                Console.Clear();
                Console.WriteLine("\n".Multiply(Console.WindowHeight / 2 - 2));
                Console.WriteLine("yed\nEva is the sole human (not piece) survivor of the Rokkenjima incident.\nThis is true for every game. Since we're not talking about pieces.".PadCenterBoth(Console.WindowWidth, Console.WindowHeight));
                Console.ReadKey();
                WarnAboutOldPerlModules();
            }));

            // Culprit question
            AddQuestion(new Question("Who is the culprit?",
            culpritsList,
            ProgramState.GetCulpritIndex(ProgramState.Instance.Options.Culprit),
            () =>
            {
                Console.Clear();
                Console.Write("Correcto.".PadCenterBoth(Console.WindowWidth, Console.WindowHeight));
                Console.ReadKey();
                Console.ForegroundColor = ConsoleColor.Red;
            }));

            // Testing console write speed
            //int i = 0;
            //while (true)
            //{
            //    Console.SetCursorPosition(0, 0);
            //    Console.Write(i.ToString().PadCenterBoth(Console.WindowWidth, Console.WindowHeight));
            //    i++;
            //}

#if !DEBUG
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
#endif

            foreach (Question question in ProgramState.Instance.Questions)
            {
                question.RunBefore?.Invoke();
                // Console.WriteLine(ProgramState.DisplayQuestion(question));
                if (!ProgramState.DisplayQuestion(question))
                {
                    // TODO: Announce it's wrong here
                    break;
                }
            }
            Console.ReadKey();

#if WINFAG
            // ProgramState.SaveCulpritRegKey("Kanon");
            // ProgramState.Instance.ReadCulpritRegKey();
#endif
            // SpookyEffect(captured);

            Console.Clear();
            Console.WriteLine((ProgramState.Instance.Options.Culprit ?? "Hola.").PadCenterBoth(Console.WindowWidth, Console.WindowHeight));

#if WINFAG
            MessageBox.Show("The code execution cannot proceed because love.dll was not found. Reinstalling the program may fix this problem.", "07151129.exe - System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
        }

        private static void AddQuestion(Question question)
        { ProgramState.Instance.Questions.Add(question); }

        private static void WarnAboutOldPerlModules()
        {
            Console.Clear();

            Console.Write(("hola\n\n"
            + "The next question will be different (but still true) when Nome sees it.\n"
            + "This is because he hasn't finished Ep6 yet and this would be a spoiler for him.\n"
            + "In both cases the correct option will be in the same place.\n"
            + "So if you want to talk with Nome about it, you can use 'the [n]th' option to refer to the answer.\n\n"
            + "[Enter to continue]").PadCenterBoth(Console.WindowWidth, Console.WindowHeight));

            while (Console.ReadKey().Key != ConsoleKey.Enter)
            { }
        }

        // private static void SpookyEffect(OutputCapture captured)
        // {
        //     int i = 1000;
        //     while (i > 10)
        //     {
        //         SwitchConsoleBackGroundAndForegroundColors(captured);
        //         Thread.Sleep(i);
        //         i /= 2;
        //     }
        //     for (int j = 0; j < 10; j++)
        //     {
        //         SwitchConsoleBackGroundAndForegroundColors(captured);
        //         Thread.Sleep(50);
        //     }
        // }

        // private static void SwitchConsoleBackGroundAndForegroundColors(OutputCapture captured)
        // {
        //     var temp = Console.BackgroundColor;
        //     Console.BackgroundColor = Console.ForegroundColor;
        //     Console.ForegroundColor = temp;
        //     Console.Clear();
        //     Console.Write(captured.LastWritten);
        // }
    }
}
