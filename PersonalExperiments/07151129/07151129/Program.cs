using CommandLine;

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
            OptionsHolder.Instance.Options = parser.ParseArguments<Options>(args).Value;
        }
    }
}

