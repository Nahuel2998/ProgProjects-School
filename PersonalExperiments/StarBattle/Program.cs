using System;
using System.Text;

namespace StarBattle
{
    internal static class Program
    {
        private const int Left = 1;
        private const int Up = 2;
        private const int Right = 4;
        private const int Down = 8;

        private static readonly char[] GetCorner = new[]
        {
            ' ',    // No Corners
            '╴',    // LEFT 
            '╵',    // UP
            '┘',    // LEFT UP
            '╶',    // RIGHT
            '─',    // LEFT RIGHT
            '└',    // UP RIGHT
            '┴',    // LEFT UP RIGHT
            '╷',    // DOWN
            '┐',    // LEFT DOWN
            '│',    // UP DOWN
            '┤',    // LEFT UP DOWN
            '┌',    // RIGHT DOWN
            '┬',    // LEFT RIGHT DOWN
            '├',    // UP RIGHT DOWN
            '┼'     // LEFT UP RIGHT DOWN
        };

        private static string GenerateBoardBorders(char[,] board)
        {
            // board's bottom corner values
            var maxI = board.GetLength(0) - 1;
            var maxJ = board.GetLength(1) - 1;

            // Generating res Canvas
            var res = new char[(maxI + 1)*2 + 1, (maxJ + 1)*2 + 1];

            // Generate Vertical Splits
            for (var i = 0; i <= maxI; i++)
                for (var j = 1; j <= maxJ; j++)
                    if (board[i, j] != board[i, j - 1])
                        res[i*2 + 1, j*2] = '│';

            // Generate Horizontal Splits
            for (var j = 0; j <= maxJ; j++)
                for (var i = 1; i <= maxI; i++)
                    if (board[i, j] != board[i - 1, j])
                        res[i*2, j*2 + 1] = '─';

            // Switching to res' values
            maxI = res.GetLength(0) - 1;
            maxJ = res.GetLength(1) - 1;

            // Generate Vertical Borders
            for (var i = 1; i < maxI; i++)
                res[i, 0] = res[i, maxJ] = '│';

            // Generate Horizontal Borders
            for (var j = 1; j < maxJ; j++)
                res[0, j] = res[maxI, j] = '─';

            // Generate Corners
            for (var i = 0; i <= maxI; i += 2)
                for (var j = 0; j <= maxJ; j += 2)
                {
                    var cornerVal = 0;

                    if (i != 0)
                        if (!res[i - 1, j].Equals('\0'))
                            cornerVal += Up;
                    if (j != 0)
                        if (!res[i, j - 1].Equals('\0'))
                            cornerVal += Left;
                    if (i != maxI)
                        if (!res[i + 1, j].Equals('\0'))
                            cornerVal += Down;
                    if (j != maxJ)
                        if (!res[i, j + 1].Equals('\0'))
                            cornerVal += Right;

                    res[i, j] = GetCorner[cornerVal];
                }

            var realres = new StringBuilder(string.Empty);
            for (var i = 0; i <= maxI; i++)
            {
                for (var j = 0; j <= maxJ; j++)
                    realres.Append(res[i, j].Equals('\0') ? ' ' : res[i, j]);
                realres.Append('\n');
            }

            return realres.ToString();
        }

        private static void Main()
        {
            // TODO: Board Generator.
            // Maybe use Dict to check if a region is used?
            // Actually, a bool array might be much better
            // yeah
            var board = new[,]
            {
                {'A', 'B', 'C', 'D', 'D'},
                {'A', 'B', 'C', 'C', 'D'},
                {'A', 'C', 'C', 'B', 'D'},
                {'A', 'A', 'C', 'B', 'D'},
                {'A', 'B', 'B', 'B', 'B'}
            };
            var thething = GenerateBoardBorders(board);
            Console.WriteLine(thething);
        }
    }
}
