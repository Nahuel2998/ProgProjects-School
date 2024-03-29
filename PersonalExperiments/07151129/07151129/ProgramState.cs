﻿using CommandLine;
using NarLib;
#if WINFAG
using Microsoft.Win32;
using System.Media;
using System.Reflection;
#endif

namespace _07151129
{
    public sealed class ProgramState
    {
        public Options Options { get; set; } = new Options();
        public List<Question> Questions = new();
#if WINFAG
        public SoundPlayer? HopePlayer { get; set; }
        public SoundPlayer? FinalPlayer { get; set; }
        public SoundPlayer? BeatoPlayer { get; set; }
        public SoundPlayer? ChainsPlayer { get; set; }
#endif

        public static ProgramState Instance { get; } = new();
        static ProgramState() { }
        private ProgramState()
        { }

#if WINFAG
        // Attempt to save the culprit on the windows registry
        public static void SaveCulpritRegKey(string culprit)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey("YOSANKAEVA").CreateSubKey("07151129");
                key.SetValue("Culprit", culprit);
                key.Close();
            }
            catch (Exception)
            { /* PlanB(culprit); */ }
        }

        // If saving a regKey failed, replace the .lnk instead and use the parameter
        // You know what I trust it won't fail
        // public static void PlanB(string culprit)
        // { }

        // Read the culprit regkey and use it over the parameter if it exists 
        public void ReadCulpritRegKey()
        {
            try
            {
                using RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"YOSANKAEVA\07151129");
                if (key != null)
                {
                    object? culprit = key.GetValue("Culprit");
                    if (culprit != null)
                    { Options.Culprit = culprit.ToString(); }
                }
            }
            catch (Exception)
            { }
        }
#endif

        public static bool DisplayQuestion(Question question)
        { return DisplayQuestion(question.Title, question.Choices, question.AnswerIndex, question.RunIfAnswerIndexIs, question.CustomDrawFunc, question.RunWithAnsAsParameter); }

        public static bool DisplayQuestion(string title, string[] choices, int answerIndex,
        Tuple<int, Action>? runIfAnswerIndexIs = null, Func<string, string[], int>? customDrawFunc = null,
        Action<int>? runWithAnsAsParameter = null)
        {
            int ans = customDrawFunc?.Invoke(title, choices) ?? DisplayQuestion(title, choices);

            if (ans == runIfAnswerIndexIs?.Item1)
            { runIfAnswerIndexIs.Item2.Invoke(); }

            runWithAnsAsParameter?.Invoke(ans);

            return answerIndex == ans || answerIndex == -1;
        }

        public static int DisplayQuestion(string title, string[] choices)
        {
            return Menu.BuildMenuGetIndex(title, choices,
                cancellable: false, centered: true, windowWidth: Console.WindowWidth - 1, windowHeight: Console.WindowHeight);
        }

        public static int GetCulpritIndex(string? culpritName) => culpritName switch
        {
            "Ushiromiya Kinzo" => 0,
            "Ushiromiya Krauss" => 1,
            "Ushiromiya Natsuhi" => 2,
            "Ushiromiya Jessica" => 3,
            "Ushiromiya Eva" => 4,
            "Ushiromiya Hideyoshi" => 5,
            "Ushiromiya George" => 6,
            "Ushiromiya Rudolf" => 7,
            "Ushiromiya Kyrie" => 8,
            "Ushiromiya Battler" => 9,
            "Ushiromiya Rosa" => 10,
            "Ushiromiya Maria" => 11,
            "Nanjo Terumasa" => 12,
            "Ronoue Genji" => 13,
            "Shannon" => 14,
            "Kanon" => 15,
            "Gohda Toshiro" => 16,
            "Kumasawa Chiyo" => 17,
            null => -1,
            _ => throw new ArgumentException("literally who")
        };
    }

    public class Options
    {
        [Option('h', "help", HelpText = "Help.")]
        public bool Help { get; set; }

        [Option("please", HelpText = "Please use please.")]
        public bool Please { get; set; }

        [Option("nome", HelpText = "Do not spoil.")]
        public bool Nome { get; set; }

        [Option('c', "culprit", HelpText = "The Culprit is Hideyoshi eating a donut awkwardly slowly.")]
        public string? Culprit { get; set; }
    }
}
