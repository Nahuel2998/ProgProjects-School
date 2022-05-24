namespace ModSelector
{
    public class OptionsHolder
    {
        public Options Options { get; set; }
        public static readonly OptionsHolder Instance = new();

        private OptionsHolder()
        { }
    }
}