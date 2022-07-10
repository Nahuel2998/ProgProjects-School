using CommandLine;
#if WINFAG
using Microsoft.Win32;
#endif

namespace _07151129
{
    public sealed class ProgramState
    {
        public Options Options { get; set; } = new Options();

        private static readonly ProgramState instance = new();

        static ProgramState() { }
        private ProgramState() { }

        public static ProgramState Instance
        { get { return instance; } }

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
            { PlanB(culprit); }
        }
#endif

        // If saving a regKey failed, replace the .lnk instead and use the parameter
        public static void PlanB(string culprit)
        {
            // TODO: me
        }

#if WINFAG
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
    }

    public class Options
    {
        [Option('h', "help", HelpText = "Help.")]
        public bool Help { get; set; }

        [Option("please", HelpText = "Please use please.")]
        public bool Please { get; set; }

        [Option('c', "culprit", HelpText = "The Culprit is Hideyoshi eating a donut awkwardly slowly.")]
        public string? Culprit { get; set; }
    }
}

