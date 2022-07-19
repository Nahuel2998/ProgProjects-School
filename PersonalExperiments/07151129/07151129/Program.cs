﻿using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
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
                    "Ushiromiya Batler",
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

            Console.SetWindowSize(Console.WindowWidth + 1, 40);
            Console.BufferHeight = 40;
            // Console.BufferWidth = Console.WindowWidth + 1;
#endif
            Console.CursorVisible = false;

            // TODO: Add freno for loading screen.

            AddQuestion(new Question("What's the name of the red haired dude who argues with the witch?",
            "Ushiromiya Butter : Ushiromiya Battler : Ushiromiya Batter : Ushiromiya Batler : Ushiromiya Butler",
            3, runIfCorrect:
            () => ClearSayAndWaitCentered("yeah")));

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
            () => ClearSayAndWaitCentered("yes")));

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
                Console.WriteLine("Acknowledged.".PadCenterHorizontal(Console.WindowWidth - 1));
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }));

            AddQuestion(new Question("What is the magic repellent Maria gives?",
            "Butterfly Brooch : Family Head Ring : Scorpion Charm : Letter of the Golden Witch",
            2, runIfCorrect:
            () => ClearSayAndWaitCentered(">be goruden witchi\n>be scared of scorpions")));

            AddQuestion(new Question("What's the name of the second mansion?",
            "Rokkenjima : Kuwadorian : Kumasawa : There's only one mansion",
            1, runIfCorrect:
            () => ClearSayAndWaitCentered("y si")));

            AddQuestion(new Question("What's the only Knox rule purposely ommited in the story?",
            "Knox's 1st : Knox's 2nd : Knox's 3rd : Knox's 4th : Knox's 5th : Knox's 6th : Knox's 7th : Knox's 8th : None",
            4, runIfCorrect:
            () => ClearSayAndWaitCentered("Knox's 5th: No chinaman must figure in the story.\n\nI wonder why it was omitted.")));

            AddQuestion(new Question("What does Maria love to say?",
            "uooooooooh!:uu-uu!:auau!:",
            1, runIfAnswerIndexIs:
            new Tuple<int, Action>(3,
            () => ClearSayAndWaitCentered("jjjjj\nGet it because she's muted.\nSo ye still wrong.")), runIfCorrect:
            () => ClearSayAndWaitCentered("uu-uu!")));

            AddQuestion(new Question("According to Episode Introductions, what's the hardest Episode?",
            "Legend of the Golden Witch : Turn of the Golden Witch : Banquet of the Golden Witch : Alliance of the Golden Witch : End of the Golden Witch : Dawn of the Golden Witch : Trivia of the Golden Witch",
            1, runIfCorrect:
            () => ClearSayAndWaitCentered("It truly is")));

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
                Console.WriteLine("That's right!".PadCenterHorizontal(Console.WindowWidth - 1));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Kinzo is dead at the starting time for all games.".PadCenterHorizontal(Console.WindowWidth - 1));
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("So he didn't invite a single human.".PadCenterHorizontal(Console.WindowWidth - 1));
                Console.ReadKey();
            }));

            AddQuestion(new Question("Who's the only piece (invited to the conference)\nthat lives past October 6th 1986 across all episodes?",
            "Ushiromiya Battler : Ushiromiya Eva : Ushiromiya Ange : On the 9th Twilight, none shall be left alive",
            2, runIfCorrect:
            () => ClearSayAndWaitCentered("g\nEven if Ange didn't attend the conference, she was invited.\nShe was just sick that day.")));

            AddQuestion(new Question("Who's the only human (who attends the conference)\nthat lives past October 6th 1986 across all episodes?",
            "Ushiromiya Battler : Ushiromiya Eva : Ushiromiya Ange : On the 9th Twilight, none shall be left alive I said",
            1, runIfCorrect:
            () => ClearSayAndWaitCentered("yed\nEva is the sole human (not piece) survivor of the Rokkenjima incident.\nThis is true for every game. Since we're not talking about pieces.")));

            if (new Random().Next() % 2 == 0)
            {
                AddQuestion(new Question("But how did the candy leave the cup???\n- - - - - - - - - - - - - - - - - - - - - -",
                "None of the above : Actually they didn't : It was Ange (with a brick) : There was no candy to begin with : - - - - - - - - - - - - - - - - - - - - - - :".Split(':', StringSplitOptions.TrimEntries),
                5, runIfCorrect:
                () => ClearSayAndWaitCentered("asi mismo"), customDrawFunc:
                (title, options) => Menu.BuildMenuGetIndex(title, options, windowWidth: Console.WindowWidth - 1, cancellable: false, centered: true, separator: "")));
            }
            else
            {
                AddQuestion(new Question("But how did Kanon leave the cousin room?\n- - - - - - - - - - - - - - - - - - - - - - -",
                "None of the above : Actually he didn't : It was Ange (with a brick) : He wasn't there to begin with : - - - - - - - - - - - - - - - - - - - - - - - :".Split(':', StringSplitOptions.TrimEntries),
                5, runIfCorrect:
                () =>
                {
                    ClearSayAndWaitCentered("just like that\n\n(Also, Nome won't see this version of this (the previous) question)\n(yes there's two versions of this question with a 50% chance each)");
#if WINFAG
                    ProgramState.Instance.HopePlayer.PlayLooping();
                    new ToastContentBuilder().AddText("𝅘𝅥𝅮 hope").Show();
                }, runBefore:
                () => 
                {
                    ProgramState.Instance.ChainsPlayer.PlayLooping();
                    new ToastContentBuilder().AddText("𝅘𝅥𝅮 Eternal Chains").Show();
#endif
                }, customDrawFunc:
                (title, options) => Menu.BuildMenuGetIndex(title, options, windowWidth: Console.WindowWidth - 1, cancellable: false, centered: true, separator: "")));
            }

            // Amount of people on the island question
            AddQuestion(new Question("How many humans exist on the island?",
            ProgramState.Instance.Options.Nome
            ? "At least 20 : Exactly 19 : Exactly 18 : No more than 17"
            : "At least 19 : Exactly 18 : Exactly 17 : No more than 16",
            3,
            WarnAboutOldPerlModules,
            () => ClearSayAndWaitCentered("Correcto.")));

            AddQuestion(new Question("What's the number of humans on the island?",
            "15 : 14 : 13 : 12",
            3,
            () => ClearSayAndWaitCentered("Actually\nLet's get specific"),
            () => ClearSayAndWaitCentered("Por supuesto\n(The previous question has been presented by Ushiromiya Eva)")));

            AddQuestion(new Question("What is Natsuhi's favourite season?",
            "Spring : Summer : Autumn : Winter",
            2, runIfCorrect:
            () => ClearSayAndWaitCentered("it is")));

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
#endif
            },
            runWithAnsAsParameter:
            (ans) =>
            {
#if WINFAG
                if (ProgramState.Instance.Options.Culprit == null)
                { ProgramState.SaveCulpritRegKey(culpritsList[ans]); }
#endif
                ProgramState.Instance.Options.Culprit = culpritsList[ans];
            }, runIfCorrect:
            () =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                ClearSayAndWaitCentered("correct!");
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

            Credits();
#if WINFAG
            ProgramState.Instance.FinalPlayer.Stop();
#endif
            Epitaph();
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
            + "[Enter to continue]").PadCenterBoth(Console.WindowWidth - 1, Console.WindowHeight));

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
            + "[Enter to continue]").PadRightMultiline(Console.WindowWidth - 20).PadLeftMultiline(Console.WindowWidth - 1).PadCenterVertical());

            while (Console.ReadKey().Key != ConsoleKey.Enter)
            { }
        }

        [DoesNotReturn]
        private static void Epitaph()
        {
            Console.Write(@"
Behold the lovely program, running on your beloved computer.
You who seek the Golden Land, follow its path downstream in search of the key. 

As you travel down it, you will see a trivia.
In that trivia, look for the answers of each question.
There sleeps the key to the Golden Land.

The one who obtains the key must then travel to the Golden Land in accordance with these rules. 

On the first twilight, offer the first seven chosen by the key.
On the second twilight, those who remain shall be split in half.
On the third twilight, those on the left shall join the world of offerings.
On the fourth twilight, gouge the head and subtract.
On the fifth twilight, gouge the chest and subtract.
On the sixth twilight, gouge the stomach and subtract.
On the seventh twilight, gouge the knee and subtract.
On the eighth twilight, gouge the leg and subtract.
On the ninth twilight, the witch shall revive, and the culprit shall be the only left unused.
On the tenth twilight, the journey shall end, and you shall reach the capital where the gold dwells.

The witch will praise the wise and bestow four treasures.
One shall be all the gold in the Golden Land.
One shall be all the gold in the Golden Land.
One shall be all the gold in the Golden Land.
One shall be all the gold in the Golden Land.

Rest in peace,
My beloved witch,
Freno.
".PadCenterBoth());
            while(true)
            { }
        }

        private static void Credits()
        {
            string credits = @"
Umineko no Naku Koro ni
~ Understanding of Observance and Hope ~

Episode ???
Trivia of the Golden Witch


[STAFF]

+ Supervision +
Ryukishi07

+ Original Picture GFX +
Nahuel2998

+ GFX Supervision +
Nahuel2998
Nomedigas

+ Graphics +
Nahuel2998
-Sprites-
Nahuel2998
-Event CGs-
Nahuel2998
-Event CG Assistance-
Nahuel2998

+ Background Art +
Nahuel2998
-External Staff-

+ Development +
-Programming-
Nahuel2998
-Script-
Nahuel2998
Nomedigas
Ushiromiya Eva
Dlanor A. Knox
-Effects-
Nahuel2998
-Support-
Furudo Erika
Stackoverflow

+ Cast +
Ushiromiya Batler as Incompetent
Ushiromiya Kinzo as Dead
Ushiromiya Krauss as Boxer
Ushiromiya Natushi as Cute
Ushiromiya Jessica as Insufferable
Ushiromiya Eva as Survivor
Ushiromiya Hideyoshi as Mastermind
Ushiromiya Joji as The Golden Witch
Ushiromiya Rudolf as Bastard
Ushiromiya Kyrie as Overpowered
Ushiromiya Rosa as The Black Witch
Ushiromiya Maria as uu-uu!
Ronoue Genji as Furniture
Shannon as Furniture
Kanon as Furniture
Gohda Toshiro as The Other Culprit
Kumasawa Chiyo as Furniture
Nanjo Terumasa as Literally Who
Amakusa Juuza as Basado
Okonogi Tetsuro as Recycled
Capt. Kawabata as El Guia
Prof. Ootsuki as El Traductor
Beatrice as
The Witch of the Painting as Deceased
Lucifer as Furniture
Leviathan as Furniture
Satan as Ahot (According to Nomedigas)
Belphegor as Acute
Mammon as Furniture
Beelzebub as Furniture
Asmodeus as Furniture
Chiester410 as Hot
Chiester45 as Also Hot
Chiester00 as Unhot
Virgilia as Cutee
Ronove as Best
Gaap as Beatrice
Eva Beatrice as uoh
Sakutarou as See, ...it's magic, right?
Bernkastel as Burnt Castle
Lambdadelta as Best Girl
Furudo Erika as In terms of male human and female detective breeding, Ep5 char is the most compatible detective for humans. Not only are they an intellectual rapist, as they call so themselves, she's some inches tall and may weigh pounds. this means they're large enough to be able to handle human dicks, and with their impressive Base stats for HP and access to Duct Tape, you can be rough with them. Due to their mostly water based biology, there's no doubt in my mind that an aroused detective would be incredibly wet, so wet that you could easily have sex with one for hours without getting sore. They can also learn the moves Lock, Sadist eyes, Chopsticks, harm and Tape Whip along with not having to hide, so it'd be incredibly easy for them to get you in the mood. With their abilities Reason and Solve, they can easily recover from fatigue with enough chopsticks. No other character comes close with this level of compatibility. Also, fun fact, if you pull out enough, you can make your detective turn white. Ep5 char is literally built for human dick. Ungodly defense stat + high HP pool + Duct Tape means it can take cock all day, all shapes and sizes and still come for more.
Dlanor A. Knox as UOOOOOOOOOOOOOOOOOH
Let it be known Gertrude is Furniture
Let it be known Cornelia is Furniture
19 as The Key To The Golden Land
Zepar as Prefer Not To Say
Furfur as Personalizado
Hachijo Tohya as Ryukishi07

+ Audio Supervision +
Nahuel2998

+ Audio Production +
Windows

+ Audio Director +
Nahuel2998

+ Audio Producers +
Windows

+ Casting Manager +
Nahuel2998

+ Recording Studio +
Rokkenjima

+ Recording +
Yoshida Ryoko

+ Sound Editing +
Yoshida Ryoko

Read Machikado Mazoku

+ BGM +
dai
also dai
who the fuck made eternal chains
dai again

+ BGM Mastering Studio +
Kinda got tired of filling these

+ BGM Mastering Engineer +
still me btw

+ Sound Effects +
Windows

+ Opening Theme Song +
-Lyrics-
-Composer-
-Arrangement-
-Vocal-

+ Insert Theme +
We were gonna have Daddy Yankee but f

+ Opening Movie +
Nahuel2998

+ Effect Movies +
Nahuel2998

+ Package Design +
Nahuel2998

+ Marketing +
Nomedigas
Ela

+ Promotional Site Production +
Ela

+ Promotional Footage +
Ela

+ Sales +
sale

+ Special Thanks +
And You

+ Producer +
Nahuel2998

+ Executive Producer +
Nahuel2998


[Original Work]
07th Expansion


[Produced By]

 .°*°...*°**°....°°.°..°.°*******°.....°°°°°....° .°..°****°°°°. ............ ...
 ..°°...*°**°..°.°°... ...°°°°°°°. °***********°° .....°°°°°°°..  ........... ...
 ..°°...**°°°... .°°°°°°°°*********OOOOOOOOOOOOOO**********°°°°.°°°.......... ...
 ..°°...°°°°°°***ooOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOoo**°°.... ...
 ...°°**ooooOOOOOoOOOOOOOOOOOOOO#OO###############OOOOO###OOo*****ooOOOOO° . ....
 *oooOOOOoooOOOOOO#############################OO#####Oo*°........°°°*ooO° ......
 OOOOoOOO#########################################Oo*°...°*oOO###OOOOoooO° ......
 Oo*°°°..°°*oO##########################OOOOOOo*°...°*oOO######OOOOOOOOOO..°.....
 OoOOOOOOo*°°.°*oOOO##OOO###############Oo**°°°°*oOO##############OOOOOOO..°°....
 OOOO########Ooo*°°°***oO######################OOOOOooooooooooooOOOOOOOOO..°°. ..
 OOOO#####OOOOOOOOOOOO######################OOOOoo*****ooooooooooooOOOOOO.°°°. ..
 oOOOOOOOOoo****°°°°**oO####################Oo°......   .........°°°**oOO.°°°. ..
 .OOO#OOo*°...       ..°o###################O***ooOO°.**         .°.    °.°°°. ..
 *OOo°   .°. .....   °*°o##################OO#####@.°#O........ °#OOo °*.°°°. ..
 .Oo. .oOO#°°#o..... °##OO######################@@@° °**.....°°.°@@Ooo#o.°°°. ..
 . *Oo°°####* °°°...°*°#########################OOOO° .***°°°*o*°oO*oOOOo.°°°. ..
 . .OO#OO#@@#°.°°..°*°*OO##########################OOOoo*****°°..°*oOOOO*.°°°. ..
 .. *O#Oo**oO* .*ooOO###############################################OOOO*.°°°....
 .. .OO##O*o°*O#####################################################OOOO°.°°° ...
 ... o##############################################################OOOO°.°°° ...
 ... °##############################################################OOOO°°°°° ...
 ... .###############################################################OOO.°°°° ...
 ... .O#####################Oo######################################OOOO.°°°° ...
 .... O#####################oO######################################OOOO.°°°° ...
 .... o######################O######################################OOOo.°°°. ...
 .... *#############################################################OOOo.°°°. ...
 .... °############################################################OOOO*.°°°. ...
 .... .O###########################################################OOOO*°°°°. ...
 ..... o##########################################################OOOOO°°°°° ...°
 ..... .O##########################################O#############OOOOOO°°°°° ...°
 ...... °########################################O**O############OOOOOO.°°°° ...°
 ....... *#################OoooOOOOOOooOOOOOOooo**oO############OOOOOOO.*°°° ...°
 ........ °#################OooooOOOO###OOOOOOO################OOOOOOOo.*°°° ...°
 ......... .o#################################################OOOOOOOO*.*°°. ..°°
 .........   °O#####################O*oOOOO##################OOOOOOOO*.°*°°. ..°°
 ......... ..  °o####################OOOOO##################OOOOOOOo°*°°*°°....°°
 ......... ....  °o#######################################OOOOOOOo°°*o.°*°° ...°°
 ......... ......  .*O##################################OOOOOOO*°°*ooo.**°° ...°°
 ........  ........   °oO#############################OOOOOOo*°°*o****.*°°° ..°°°
 ........  ........ ..  .°o#########################OOOOOOo°°**o*°*oo..*°°. ..°°°


........°°°°.......                                                                                                                                                            
.......°°°°°.......                                                                                                                                                            
.......°°°°°°.....                                                                                                                                                             
 ......°°°°°°.....                                                                                                                                                             
 .......°°°°...                        ..                                                                                                                                      
  ............ °°°. ***°   °***       *@o                                                             **** .***°                              °°°.                            
   ............#@#. °@#.   .*@°        #*      .#°                             .#°                    .##°  *@*.  #°                          o@@*                             
    ...........##. . Oo      O         O*       *             ..                *                      oO   o°    *.                           O#*                             
     ........  #O    OO     .O    .    O*                   .. ..                                      o#  o°                                  °@*                      .....  
               #*    Oo     .o   **oo  Oo*OO.  *O° .o*°#° .**oO.  *O°*#o.*OO.  *O. oOo .oO° °o#°       oO *°     *O° .oo°O#°  °O**O°  .**OO     #*                     ....... 
               #°    Oo     .o  *o  °  Oo  OO  .#°  OO°*  O°  oO  °#* .#o. Oo  .#°  Oo  o* °° *#       oOoO      .#°  OO. o#  *. *#   O°  OO    O*                     .°°°... 
               #.    oo     .o  °#*    O°  °O   O°  oo   °O   .#°  #   O*  °O   O.  °#  *     .O.      oO.#*      O°  *o  .#.    #°  *O   .#.   O*                     ..°°... 
               O°    oo     .*   °#O.  o*  °O   O.  oo   °O    O° .O.  o*  °O   O.   O°.°   .°*O       *O °#*     O°  *o  .O.   oo   *O   .O.   O°                     ......  
               O*    *O     °°  .  oo  o°  °O   O.  **   .O.   O.  O   o*  °o   O.   *o*   *o  O       *o  °#°    O.  *o  .O.  °O  . °O   .O   .O°   ....               ....   
               oo     Oo.  °*   *. °o  o*  °O  .O°  oo    oo  °*  .O.  o*  °O  .O°    O°   *o.°O°      oO   °O*  .O°  oo  .O.  O*  *  oo  *°   °O° .......                .    
               oO*     *oo*°    .*°°  °**. °*° °** .**°    °*°.   °*° .**. **° °*°    *     **.**     °**°   .**.°** .**. °*° .*°°*°   **°.   .OO° ..°°....                    
               ***.                                                                  °°                                                       °**..°°°°°...                    
                                                                                     o  .....                                                     .°°°°°°...                   
                                                                                    o° ........                                                 ...°°°°°°...                   
                                                                                    ° ...°°.....                                                 ..°°°°°....                   
                                                                                     ..°°°°°°....                                                ....°°....                    
                                                                                   ...°°°°°°°°...                                                 .........                    
                                                                                   ...°°°°*°°°°...                                                 .......                     
        *°.o                        °...       .°.°                                ...°°°***°°°...              .o#°                                 ...         O*°#.         
       *o O.                       .*@#°   o.  .o#o.                               ...°°°***°°. ..               °@.                                             *O #°         
       *#.#o                         o#    @o   .O                                 ..°°°°°°°°.°° .               .#.                                             *..o          
        ° ..                         °@.  °##   **                                    .°°°°°° #*                 .#.                                            .. °           
                                      #*  o°@°  o.   .*oo.  .*°°Oo .oO°    °*O.  °*.*O* °°°°.°#Oo° **.O°.o° °**  .#. *o* °oo.     *O*   °*.O*  °*O°                            
                                      OO .* Oo .o   °O. *#. .#O°°OO°°o#   o* *#  *@*°*@°......#*°. O#*o° #*  o#  .#. °#*  O*     o.°#°  o#*o° ** °#.                           
                                      °# °° *# °°   O°   Oo  o*  °O   O. .#°.°#° .#   O* .... O°   °O    o*  °O  .#.  oO  *         o*  °O    O°..#*                   .....   
                                      .#°*   #°*.   O°   *o  o*  °O   O° °O°°°.  .O.  o* .    O°   *o    o*  °O  .O.  .O °°       .°O*  °O   .O°°°.                  ........  
                                       oO°   oO*    O*   *o  o*  °O   O. .O      .O   o*      o°   *o    o*  °o  .O.   o**      .o° o°  °O   .O.                    ...°°°°... 
              **  o° .o. .o  °*  **    *O    °#°    *O   o.  o*  °O   O.  oo. .. .O.  o*      o*   *o    *o  *o  .O.   °O°      .O. o*  °O    oO. .. °*  **  *°     ..°°°°°°...
              o*  o° .O. °o. *o  o*    .*     o      *o°°.  .o*. *o° °o*   ooo°  *o* .o*.     °o*..*o°   .oo°°o° *o*    o        *o°*o. *o°    *oo°  °o  *o  o*    ...°°**°°°..
                                                                                                                       °°                                          ..°°°***°°..
                                                                                                                       o                                           ..°°°**°°°..
                                                                                                                      o°                                           ...°°°°°°°..
                                                                                                                     .*                                             ...°°°°°.. 
                                                                                                                                                                     ......... 
                                                                                                                                                                      ......   
                                                                                                                                                                         .     
";
            credits = Regex.Replace(credits, $"(?<={ProgramState.Instance.Options.Culprit} as ).*$", "The Culprit", RegexOptions.Multiline);

#if WINFAG
            Console.WindowWidth = Console.LargestWindowWidth - 2;
            // Console.BufferWidth = Console.LargestWindowWidth;
            TextScroller.ScrollText1(credits.PadCenterHorizontal(Console.WindowWidth - 1), 100);
#else
            TextScroller.ScrollText(credits.PadCenterHorizontal(Console.WindowWidth - 1), 100);
#endif
        }

        private static void ClearSayAndWaitCentered(string message)
        { ClearSayAndWait(message.PadCenterBoth(Console.WindowWidth - 1, Console.WindowHeight)); }

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
