using System;

namespace NarLib
{
    public static class TextScroller
    {
        public static void ScrollText(string text, int interval = 200, int? windowHeight = null)
        {
            int height = windowHeight ?? Console.WindowHeight;
            string padding = new('\n', height);

            string[] data = (padding + text + padding).Split('\n');
            string[] frame = new string[height];

            Console.Clear();

            for (var i = 0; i < data.Length - height; i++)
            {
                Console.SetCursorPosition(0, 0);

                Array.Copy(data, i, frame, 0, height);
                Console.Write(string.Join('\n', frame));

                Thread.Sleep(interval);
            }
        }
    }
}
