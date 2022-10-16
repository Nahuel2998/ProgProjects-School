using AsciiRenderer.Classes;

namespace AsciiRenderer
{
    internal static class Program
    {
        static void Main()
        {
            Board board = new(20, 10);

            board.DrawHorizontalLine("hola", 14, 2);
            board.DrawVerticalLine(Shape.Line(5, '>', 'v', '^'), 2, 2);

            board.Print();
        }
    }
}