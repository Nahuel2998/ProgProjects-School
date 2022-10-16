using System;
using NarExtensions;

namespace AsciiRenderer.Classes
{
    public class Board
    {
        public char[,] BoardMatrix { get; }

        // Props returning each corner's coordinates?
        public int XLength => BoardMatrix.GetLength(1);
        public int YLength => BoardMatrix.GetLength(0);

        public Board(int width, int height)
        {
            BoardMatrix = new char[height, width];
            BoardMatrix.SpanFill(' ');
        }

        public Board(char[,] board)
        { BoardMatrix = board; }

        public void DrawHorizontalLine(char[] data, int x, int y)
        {
            if (XLength - x < data.Length)
            { throw new ArgumentException("data too long to place at this position."); }

            Buffer.BlockCopy(data, 0, BoardMatrix, ((XLength * y) + x) * sizeof(char), data.Length * sizeof(char));
        }

        public void DrawHorizontalLine(string data, int x, int y)
        { DrawHorizontalLine(data.ToCharArray(), x, y); }

        public void DrawVerticalLine(char[] data, int x, int y)
        {
            if (YLength - y < data.Length)
            { throw new ArgumentException("data too long to place at this position."); }

            for (int i = y, iter = 0; iter < data.Length; i++, iter++)
            { BoardMatrix[i, x] = data[iter]; }
        }

        public void DrawVerticalLine(string data, int x, int y)
        { DrawVerticalLine(data.ToCharArray(), x, y); }

        public void Print()
        {
            for (int i = 0; i < BoardMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < BoardMatrix.GetLength(1); j++)
                { Console.Write(BoardMatrix[i, j]); }

                Console.WriteLine();
            }
        }

        public override string ToString()
        { return BoardMatrix.ToMatrixString(); }
    }
}