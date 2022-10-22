using System;
using System.Collections.Generic;

namespace AsciiRenderer
{
    public class QueueBoard : Board
    {
        private readonly Queue<(int, int, char)> _changes = new();

        public QueueBoard(int width, int height) : base(width, height)
        { }

        public QueueBoard(char[,] board) : base(board)
        { }

        public override void EditAt(int x, int y, char character)
        {
            base.EditAt(x, y, character);
            _changes.Enqueue((x, y, character));
        }

        public void PrintUpdated()
        {
            while (_changes.Count > 0)
            {
                (int, int, char) result = _changes.Dequeue();

                Console.SetCursorPosition(result.Item1, result.Item2);
                Console.Write(result.Item3);
            }
        }
    }
}