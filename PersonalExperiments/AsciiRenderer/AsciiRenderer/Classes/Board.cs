using System;
using NarExtensions;

namespace AsciiRenderer
{
    public class Board
    {
        public readonly char[,] BoardMatrix;

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

        public bool Occupied((int, int) xy)
        { return Occupied(xy.Item1, xy.Item2); }

        public bool Occupied((int, int, int, int) fXfYtXtY)
        { return Occupied(fXfYtXtY.Item1, fXfYtXtY.Item2, fXfYtXtY.Item3, fXfYtXtY.Item4); }

        public bool Occupied(int x, int y)
        { return BoardMatrix[y, x] != ' '; }

        public bool Occupied(int fromX, int fromY, int toX, int toY)
        {
            for (int x = fromX; x <= toX; x++)
            {
                for (int y = fromY; y <= toY; y++)
                {
                    if (!Occupied(x, y))
                    { return true; }
                }
            }

            return false;
        }

        public void EditAt((int, int) xy, char character)
        { EditAt(xy.Item1, xy.Item2, character); }

        public void EditAt(int x, int y, char character)
        { BoardMatrix[y, x] = character; }

        public char GetAt(int x, int y)
        { return BoardMatrix[y, x]; }

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