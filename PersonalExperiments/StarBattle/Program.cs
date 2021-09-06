using System;
using System.Text;

namespace StarBattle
{
    class Program
    {
        const int LEFT = 1;
        const int UP = 2;
        const int RIGHT = 4;
        const int DOWN = 8;

        static char[] getCorner = new char[]
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

        static string GenerateBoardBoarders(char[,] board)
        {
            char[,] res = new char[,]
            {
                {'┌', '─', '─', '─', '─', '─', '─', '─', '┐'},
                {'│', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '│'},
                {'│', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '│'},
                {'│', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '│'},
                {'│', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '│'},
                {'│', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '│'},
                {'│', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '│'},
                {'│', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '│'},
                {'└', '─', '─', '─', '─', '─', '─', '─', '┘'}
            };
            
            // board's bottom corner values
            int maxI = board.GetLength(0) - 1;
            int maxJ = board.GetLength(1) - 1;

            // Generate Vertical Splits
            for (int i = 0; i <= maxI; i++)
                for (int j = 1; j <= maxJ; j++) 
                    if (board[i, j] != board[i, j - 1])
                        res[i*2 + 1, j*2] = '│';

            // Generate Horizontal Splits
            for (int j = 0; j <= maxJ; j++)
                for (int i = 1; i <= maxI; i++) 
                    if (board[i, j] != board[i - 1, j])
                        res[i*2, j*2 + 1] = '─';

            // Switching to res' values
            maxI = res.GetLength(0) - 1;
            maxJ = res.GetLength(1) - 1;

            // Generate Corners
            for (int i = 0; i <= maxI; i += 2)
                for (int j = 0; j <= maxJ; j += 2)
                {
                    int cornerVal = 0;

                    if (i != 0)
                        if (!res[i - 1, j].Equals(' ')) 
                            cornerVal += UP;
                    if (j != 0)
                        if (!res[i, j - 1].Equals(' ')) 
                            cornerVal += LEFT;
                    if (i != maxI)
                        if (!res[i + 1, j].Equals(' ')) 
                            cornerVal += DOWN;
                    if (j != maxJ)
                        if (!res[i, j + 1].Equals(' ')) 
                            cornerVal += RIGHT;

                    res[i, j] = getCorner[cornerVal];
                }

            StringBuilder realres = new StringBuilder(string.Empty);
            for (int i = 0; i <= maxI; i++)
            {
                for (int j = 0; j <= maxJ; j++)
                    realres.Append(res[i, j]);
                realres.Append("\n");
            }

            return realres.ToString();
        }

        static void Main(string[] args)
        {
            // TODO: Board Generator.
            // Maybe use Dict to check if a region is used?
            // Actually, a bool array might be much better
            // yeah
            char[,] board = new char[,]
            {
                {'A', 'B', 'C', 'D'},
                {'A', 'B', 'C', 'C'},
                {'A', 'C', 'C', 'B'},
                {'A', 'A', 'C', 'B'}
            };
            string thething = GenerateBoardBoarders(board);
            Console.WriteLine(thething);
        }
    }
}
