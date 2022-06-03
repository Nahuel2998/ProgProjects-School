using CommandLine;

namespace _07151129
{
    public sealed class OptionsHolder
    {
        public Options? Options { get; set; }

        private static readonly OptionsHolder instance = new();

        static OptionsHolder() { }
        private OptionsHolder() { }

        public static OptionsHolder Instance
        { get { return instance; } }
    }

    public class Options
    {
        [Option('h', "help", HelpText="Help.")]
        public bool Help { get; set; }

        [Option("please", HelpText="Please use please.")]
        public bool Please { get; set; }

        [Option('c', "culprit", HelpText="The Culprit is Hideyoshi eating a donut awkwardly slowly.")]
        public string? Culprit { get; set; }
    }
}

