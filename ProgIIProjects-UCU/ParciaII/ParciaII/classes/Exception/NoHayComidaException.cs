namespace ParciaII.classes.Exception
{
    public class NoHayComidaException : System.Exception
    {
        public NoHayComidaException()
        { }

        public NoHayComidaException(string message) : base(message)
        { }
        
        public NoHayComidaException(string message, System.Exception inner) : base(message, inner)
        { }
    }
}