using CommandLine;
using NarExtensions;
using NarLib;
#if WINFAG
using System.Media;
using System.Reflection;
#endif

namespace _07151129
{
    internal static class Program
    {
        static readonly string[] culpritsList =
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

#if WINFAG
            ProgramState.Instance.ReadCulpritRegKey();

            ProgramState.Instance.HopePlayer = new SoundPlayer(Assembly.GetExecutingAssembly().GetManifestResourceStream("_07151129.esperanza.wav"));
            ProgramState.Instance.HopePlayer.Load();
            ProgramState.Instance.FinalPlayer = new SoundPlayer(Assembly.GetExecutingAssembly().GetManifestResourceStream($"_07151129.{(ProgramState.Instance.Options.Nome ? "RespuestaFinalCompleta" : "VIVO")}.wav"));
            ProgramState.Instance.FinalPlayer.Load();
            ProgramState.Instance.BeatoPlayer = new SoundPlayer(Assembly.GetExecutingAssembly().GetManifestResourceStream("_07151129.ahaha.wav"));
            ProgramState.Instance.BeatoPlayer.Load();

            ProgramState.Instance.HopePlayer.Play();
#endif

            // TODO: Add freno for loading screen.

            Console.CursorVisible = false;
#if WINFAG
            Console.SetWindowSize(Console.WindowWidth, 40);
#endif
            AddQuestion(new Question("What's the name of the second mansion?",
            "Rokkenjima : Kuwadorian : Kumasawa : There's only one mansion",
            1, runIfCorrect:
            () => ClearSayAndWait("y si".PadCenterBoth())));

            // TODO: Change this to something else.
            // True identity of the witch of the forest?
            Menu.BuildMenuGetIndex("What does Maria love to say?".PadCenterHorizontal(Console.WindowWidth) + "\n"
                    + "- - - - - - - - - - - - - - - - -".PadCenterHorizontal(Console.WindowWidth),
                    "uooooooooh!:uu-uu!:auau!:- - - - - - - - - - - - - - - - -: ".Split(':'),
                cancellable: false, centered: true, windowWidth: Console.WindowWidth, windowHeight: Console.WindowHeight,
                separator: "");

            AddQuestion(new Question("What does Maria love to say?",
            "uooooooooh!:uu-uu!:auau!:",
            1, runIfAnswerIndexIs:
            new Tuple<int, Action>(3,
            () => ClearSayAndWait("jjjjj\nGet it because she's muted.\nSo ye still wrong.".PadCenterBoth())), runIfCorrect:
            () => ClearSayAndWait("uu-uu!".PadCenterBoth())));

            AddQuestion(new Question("Who's the only human (invited by Kinzo)\nthat lives past October 6th 1986 across all episodes?",
            "Ushiromiya Battler : Ushiromiya Eva : Ushiromiya Ange : None",
            3, runIfCorrect:
            () =>
            {
                Console.Clear();
                Console.WriteLine("\n".Multiply((Console.WindowHeight / 2) - 2));
                Console.WriteLine("That's right!".PadCenterHorizontal(Console.WindowWidth));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kinzo is dead at the starting time for all games.".PadCenterHorizontal(Console.WindowWidth));
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("So he didn't invite a single human.".PadCenterHorizontal(Console.WindowWidth));
                Console.ReadKey();
            }));

            AddQuestion(new Question("Who's the only piece (invited to the conference)\nthat lives past October 6th 1986 across all episodes?",
            "Ushiromiya Battler : Ushiromiya Eva : Ushiromiya Ange : On the 9th Twilight, none shall be left alive",
            2, runIfCorrect:
            () => ClearSayAndWait("g\nEven if Ange didn't attend the conference, she was invited.\nShe was just sick that day.".PadCenterBoth())));

            AddQuestion(new Question("Who's the only human (who attends the conference)\nthat lives past October 6th 1986 across all episodes?",
            "Ushiromiya Battler : Ushiromiya Eva : Ushiromiya Ange : On the 9th Twilight, none shall be left alive I said",
            1, runIfCorrect:
            () => ClearSayAndWait("yed\nEva is the sole human (not piece) survivor of the Rokkenjima incident.\nThis is true for every game. Since we're not talking about pieces.".PadCenterBoth())));

            // Amount of people on the island question
            AddQuestion(new Question("How many humans exist on the island?",
            ProgramState.Instance.Options.Nome
            ? "At least 20 : Exactly 19 : Exactly 18 : No more than 17"
            : "At least 19 : Exactly 18 : Exactly 17 : No more than 16",
            3,
            WarnAboutOldPerlModules,
            () => ClearSayAndWait("Correcto.".PadCenterBoth(Console.WindowWidth, Console.WindowHeight))));

            // Culprit question
            AddQuestion(new Question("Who is the culprit?",
            culpritsList,
            ProgramState.GetCulpritIndex(ProgramState.Instance.Options.Culprit),
            () =>
            {
#if WINFAG
                ProgramState.Instance.FinalPlayer.Play();
#endif
                Console.ForegroundColor = ConsoleColor.Red;
            }, runWithAnsAsParameter:
            (ans) =>
            {
                if (ProgramState.Instance.Options.Culprit != null)
                {
#if WINFAG
                    ProgramState.SaveCulpritRegKey(culpritsList[ans]);
#else
                    ProgramState.PlanB(culpritsList[ans]);
#endif
                }
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
                    // Paint screen white, make beato cackle, and throw the error if they miss.
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.White;
#if WINFAG
                    ProgramState.Instance.BeatoPlayer.PlaySync();
                    MessageBox.Show("The code execution cannot proceed because love.dll was not found. Reinstalling the program may fix this problem.", "07151129.exe - System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                    Environment.Exit(-1);
                }
                question.RunIfCorrect?.Invoke();
            }
#if WINFAG
            // ProgramState.SaveCulpritRegKey("Kanon");
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

        private static void ClearSayAndWait(string message)
        {
            Console.Clear();
            Console.Write(message);
            Console.ReadKey();
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
