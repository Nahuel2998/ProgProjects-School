using CommandLine;
using NarExtensions;
using NarLib;
#if WINFAG
using System.Media;
using System.Reflection;
using Microsoft.Toolkit.Uwp.Notifications;
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
            ProgramState.Instance.ChainsPlayer = new SoundPlayer(Assembly.GetExecutingAssembly().GetManifestResourceStream("_07151129.CadenasEternas.wav"));
            ProgramState.Instance.ChainsPlayer.Load();
#endif

            // TODO: Add freno for loading screen.

            Console.CursorVisible = false;
#if WINFAG
            Console.SetWindowSize(Console.WindowWidth, 40);
            Console.BufferHeight = 40;

            TextScroller.ScrollText1("hola\n\nbuenas".PadCenterHorizontal(), 50);
#else
            TextScroller.ScrollText("hola\n\nbuenas".PadCenterHorizontal(), 50);
#endif

            AddQuestion(new Question("What's the name of the red haired dude who argues with the witch?",
            "Ushiromiya Butter : Ushiromiya Battler : Ushiromiya Batter : Ushiromiya Batler : Ushiromiya Butler",
            3, runIfCorrect:
            () => ClearSayAndWait("yeah".PadCenterBoth())));

            AddQuestion(new Question("What's the name of the witch who argues with the red haired dude?",
            "Ushiromiya George : Ushiromiya Maria : Beatrice : Ushiromiya Hideyoshi : Ushiromiya Batler",
            2, runWithAnsAsParameter:
            (ans) =>
            {
                switch (ans)
                {
                    case 0: case 1: case 3: case 4:
                        ClearSayAndWaitCentered("We don't know that for sure.");
                        break;
                }
            }, runIfCorrect:
            () => ClearSayAndWait("yes".PadCenterBoth())));

            AddQuestion(new Question("What's the name of the annoying little girl who follows Bernkastel?",
            "Ushiromiya Ange : Lambdadelta : Hanyuu : Furude Rika : Furudo Erika",
            2, runWithAnsAsParameter:
            (ans) =>
            {
                switch (ans)
                {
                    case 0:
                        ClearSayAndWaitCentered("Not a little girl at the time");
                        break;
                    case 1:
                        ClearSayAndWaitCentered("rude\nShe's not annoying she cute");
                        break;
                    case 3: case 4:
                        ClearSayAndWaitCentered("Same person");
                        break;
                }
            }, runIfCorrect:
            () => ClearSayAndWaitCentered("auau\n(yeah this was more of a joke question)\n(just wanted to use auau)")));

            AddQuestion(new Question("Who's the only character capable of counting the first twilight sacrifices\nwith just one of their extremities?",
            "Ushiromiya Kinzo : Ushiromiya Natsuhi : Nanjo Terumasa : Beatrice : Furudo Erika",
            0, runIfCorrect:
            () => ClearSayAndWaitCentered("yep\nIf he were alive he could")));

            AddQuestion(new Question("In what episode was the Red Truth introduced?",
            "Legend of the Golden Witch : Turn of the Golden Witch : Banquet of the Golden Witch : Alliance of the Golden Witch : Checkmate of the Golden Witch",
            1, runIfCorrect:
            () =>
            {
                Console.Clear();
                Console.WriteLine("\n".Multiply((Console.WindowHeight / 2) - 1));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Acknowledged.".PadCenterHorizontal(Console.WindowWidth));
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }));

            AddQuestion(new Question("What's the name of the second mansion?",
            "Rokkenjima : Kuwadorian : Kumasawa : There's only one mansion",
            1, runIfCorrect:
            () => ClearSayAndWait("y si".PadCenterBoth())));

            AddQuestion(new Question("What does Maria love to say?",
            "uooooooooh!:uu-uu!:auau!:",
            1, runIfAnswerIndexIs:
            new Tuple<int, Action>(3,
            () => ClearSayAndWait("jjjjj\nGet it because she's muted.\nSo ye still wrong.".PadCenterBoth())), runIfCorrect:
            () => ClearSayAndWait("uu-uu!".PadCenterBoth())));

            AddQuestion(new Question("What's the witch of the painting true form?",
            "Butterfly : Teapot : Gold : Deceased",
            3,
            runIfCorrect:
            () => ClearSayAndWaitCentered("Correct\n\nf")));

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

            if (new Random().Next() % 2 == 0)
            {
                AddQuestion(new Question("But how did the candy leave the cup???\n- - - - - - - - - - - - - - - - - - - - - -",
                "None of the above : Actually they didn't : It was Ange (with a brick) : There was no candy to begin with : - - - - - - - - - - - - - - - - - - - - - - :".Split(':', StringSplitOptions.TrimEntries),
                5, runIfCorrect:
                () => ClearSayAndWaitCentered("asi mismo"), customDrawFunc:
                (title, options) => Menu.BuildMenuGetIndex(title, options, cancellable: false, centered: true, separator: "")));
            }
            else
            {
                AddQuestion(new Question("But how did Kanon leave the cousin room?\n- - - - - - - - - - - - - - - - - - - - - - -",
                "None of the above : Actually he didn't : It was Ange (with a brick) : He wasn't there to begin with : - - - - - - - - - - - - - - - - - - - - - - - :".Split(':', StringSplitOptions.TrimEntries),
                5,
#if WINFAG
                runBefore:
                () => 
                {
                    ProgramState.Instance.ChainsPlayer.PlayLooping();
                    new ToastContentBuilder().AddText("𝅘𝅥𝅮 Eternal Chains").Show();
                },
#endif
                runIfCorrect:
                () =>
                {
                    ClearSayAndWaitCentered("just like that\n\n(Also, Nome won't see this version of this (the previous) question)\n(yes there's two versions of this question with a 50% chance each)");
#if WINFAG
                    ProgramState.Instance.HopePlayer.PlayLooping();
                    new ToastContentBuilder().AddText("𝅘𝅥𝅮 hope").Show();
#endif
                }, customDrawFunc:
                (title, options) => Menu.BuildMenuGetIndex(title, options, cancellable: false, centered: true, separator: "")));
            }

            // Amount of people on the island question
            AddQuestion(new Question("How many humans exist on the island?",
            ProgramState.Instance.Options.Nome
            ? "At least 20 : Exactly 19 : Exactly 18 : No more than 17"
            : "At least 19 : Exactly 18 : Exactly 17 : No more than 16",
            3,
            WarnAboutOldPerlModules,
            () => ClearSayAndWait("Correcto.".PadCenterBoth(Console.WindowWidth, Console.WindowHeight))));

            AddQuestion(new Question("What's the number of humans on the island?",
            "15 : 14 : 13 : 12",
            3,
            () => ClearSayAndWaitCentered("Actually\nLet's get specific"),
            () => ClearSayAndWaitCentered("Por supuesto\n(The previous question has been presented by Ushiromiya Eva)")));

            // Culprit question
            AddQuestion(new Question("Who is the culprit?",
            culpritsList,
            ProgramState.GetCulpritIndex(ProgramState.Instance.Options.Culprit),
            () =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
#if WINFAG
                ProgramState.Instance.FinalPlayer.PlayLooping();
                new ToastContentBuilder().AddText($"𝅘𝅥𝅮 {(ProgramState.Instance.Options.Nome ? "Final Answer" : "ALIVE")}").Show();
            }, runWithAnsAsParameter:
            (ans) =>
            {
                if (ProgramState.Instance.Options.Culprit == null)
                { ProgramState.SaveCulpritRegKey(culpritsList[ans]); }
#endif
            }));

#if !DEBUG
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
#endif
            Console.WriteLine("There are currently {0} questions.", ProgramState.Instance.Questions.Count);
            Console.ReadKey();

            Introduction();
#if WINFAG
            ProgramState.Instance.HopePlayer.PlayLooping();
            new ToastContentBuilder().AddText("𝅘𝅥𝅮 hope").Show();
#endif

            foreach (Question question in ProgramState.Instance.Questions)
            {
                question.RunBefore?.Invoke();
                if (!ProgramState.DisplayQuestion(question))
                {
                    // Paint screen white, make beato cackle, and throw the error if they miss.
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
#if WINFAG
                    ProgramState.Instance.BeatoPlayer.PlaySync();
                    MessageBox.Show("The code execution cannot proceed because love.dll was not found. Reinstalling the program may fix this problem.", "07151129.exe - System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
                    Environment.Exit(19);
                }
                question.RunIfCorrect?.Invoke();
            }
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

        private static void Introduction()
        {
            Console.Clear();

            // Console.Write(("hola!!!\n"
            // + "Welcome to: 'Umineko no Naku Koro ni: Trivia of the Golden Witch.'\n\n"
            // // + "Let it be known there is no falsehood in the presented statements for future episodes either.\n"
            // // + "So there's no falsehood in \n\n"
            // // + "[Enter to continue]").PadCenterBoth(Console.WindowWidth, Console.WindowHeight));

            int[] laHora = DateTime.Now.ToString("HH:mm", System.Globalization.DateTimeFormatInfo.InvariantInfo).Split(':').Select(x => int.Parse(x)).ToArray();

            Console.Write(("Episode ???\n"
            + "Trivia of the Golden Witch\n\n"
            + (laHora[1] == 42 ? "Bonita hora.\n" : string.Format("Good {0}.\n",
            (laHora[0] >= 6 && laHora[0] < 12)
            ? "morning"
            : laHora[0] < 17
                ? "afternoon"
                : laHora[0] < 22
                    ? "evening"
                    : "night"))
            + "After countless games, all facts and hints have been fully laid on the board.\n"
            + "The Golden Witch has prepared a lovely trivia to test that knowledge of yours.\n\n"
            + "This trivia will test your general knowledge of the story up to Episode 6.\n"
            + "Let it be known there is absolutely no falsehood in the presented statements.\n"
            + "They shall remain as absolute truth for future episodes as well.\n\n"
            + "The difficulty level is only natural. You should know all of this already.\n"
            + "With love, the answers can be seen.\n\n"
            + "[Enter to continue]").PadRightMultiline(Console.WindowWidth - 20).PadLeftMultiline(Console.WindowWidth).PadCenterVertical());

            while (Console.ReadKey().Key != ConsoleKey.Enter)
            { }
        }

        private static void ClearSayAndWaitCentered(string message)
        { ClearSayAndWait(message.PadCenterBoth()); }

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
