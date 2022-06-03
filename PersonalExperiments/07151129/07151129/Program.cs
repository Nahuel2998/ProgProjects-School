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

            if (!OptionsHolder.Instance.Options.Please)
            {
                Console.WriteLine("no");
                Console.ReadKey();
                return;
            }

            if (OptionsHolder.Instance.Options.Help)
            {
                Console.WriteLine(string.Join(' ', args));
                return;
            }

            Console.WriteLine(OptionsHolder.Instance.Options.Culprit ?? "Hola.");

            // Microsoft.Win32.RegistryKey key;
            // key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("YOSANKAEVA").CreateSubKey("07151129");
            // key.SetValue("Culprit", OptionsHolder.Instance.Options.Culprit);
            // key.Close();
        }
    }
}

