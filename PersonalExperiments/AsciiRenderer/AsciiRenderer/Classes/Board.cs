using System;
using NarExtensions;

namespace AsciiRenderer
{
    public class Board
    {
        public const char EMPTY = ' ';
        public readonly char[,] BoardMatrix;

        public int XLength => BoardMatrix.GetLength(1);
        public int YLength => BoardMatrix.GetLength(0);

        // This is assuming you won't have a dimension start in anything other than 0 
        // Because why the fuck would you want that
        public (int, int) TopLeftXY => (0, 0);
        public (int, int) BottomLeftXY => (0, YLength - 1);
        public (int, int) TopRightXY => (XLength - 1, 0);
        public (int, int) BottomRightXY => (XLength - 1, YLength - 1);

        public Board(int width, int height)
        {
            BoardMatrix = new char[height, width];
            BoardMatrix.SpanFill(EMPTY);
        }

        public Board(char[,] board)
        { BoardMatrix = board; }

        public void DrawHorizontalLine(char[] data, (int, int) xy)
        { DrawHorizontalLine(data, xy.Item1, xy.Item2); }

        public void DrawHorizontalLine(string data, (int, int) xy)
        { DrawHorizontalLine(data, xy.Item1, xy.Item2); }

        public void DrawHorizontalLine(char[] data, int x, int y)
        {
            if (XLength - x < data.Length)
            { throw new ArgumentException("data too long to place at this position."); }

            Buffer.BlockCopy(data, 0, BoardMatrix, ((XLength * y) + x) * sizeof(char), data.Length * sizeof(char));
        }

        public void DrawHorizontalLine(string data, int x, int y)
        { DrawHorizontalLine(data.ToCharArray(), x, y); }

        public void DrawVerticalLine(char[] data, (int, int) xy)
        { DrawVerticalLine(data, xy.Item1, xy.Item2); }

        public void DrawVerticalLine(string data, (int, int) xy)
        { DrawVerticalLine(data, xy.Item1, xy.Item2); }

        public void DrawVerticalLine(char[] data, int x, int y)
        {
            if (YLength - y < data.Length)
            { throw new ArgumentException("data too long to place at this position."); }

            for (int i = y, iter = 0; iter < data.Length; i++, iter++)
            { BoardMatrix[i, x] = data[iter]; }
        }

        public void DrawVerticalLine(string data, int x, int y)
        { DrawVerticalLine(data.ToCharArray(), x, y); }

        // Okay I did get this from the internet
        // Maybe I'll change it later
        public void DrawLine(char data, int fromX, int fromY, int toX, int toY)
        {
            (int distX, int signX) = (Math.Abs(toX - fromX), fromX < toX ? 1 : -1);
            (int distY, int signY) = (-Math.Abs(toY - fromY), fromY < toY ? 1 : -1);
            int error = distX + distY;

            while (true)
            {
                EditAt(fromX, fromY, data);

                if (fromX == toX && fromY == toY)
                { break; }

                int err2 = error * 2;
                if (err2 >= distY)
                {
                    if (fromX == toX)
                    { break; }

                    error += distY;
                    fromX += signX;
                }
                if (err2 <= distX)
                {
                    if (fromY == toY)
                    { break; }

                    error += distX;
                    fromY += signY;
                }
            }
        }

        public bool Occupied((int, int) xy)
        { return Occupied(xy.Item1, xy.Item2); }

        public bool Occupied((int, int, int, int) fXfYtXtY)
        { return Occupied(fXfYtXtY.Item1, fXfYtXtY.Item2, fXfYtXtY.Item3, fXfYtXtY.Item4); }

        public bool Occupied(int x, int y)
        { return BoardMatrix[y, x] != EMPTY; }

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

        public virtual void EditAt(int x, int y, char character)
        { BoardMatrix[y, x] = character; }

        public void Move(int fromX, int fromY, int toX, int toY)
        {
            EditAt(toX, toY, GetAt(fromX, fromY));
            EditAt(fromX, fromY, EMPTY);
        }

        public void Move((int, int) fromXY, (int, int) toXY)
        { Move(fromXY.Item1, fromXY.Item2, toXY.Item1, toXY.Item2); }

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