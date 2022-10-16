using System;
using NarExtensions;

namespace AsciiRenderer.Classes
{
    public static class Shape
    {
        public static char[] Line(int length, char fill, char? head = null, char? tail = null)
        {
            char[] res = new char[length];

            head ??= fill;
            tail ??= head;

            Array.Fill(res, fill, 1, length - 2);

            res[0] = (char)head;
            res[length - 1] = (char)tail;

            return res;
        }

        public static char[] Line(int length, char[] fill, char[]? head = null, char[]? tail = null)
        {
            char[] res = new char[length];

            res.Fill(fill);

            tail ??= head;

            if (head?.Length + tail?.Length > length)
            { throw new ArgumentException("Sum of head and tail larger than length."); }

            if (head != null)
            { Array.Copy(head, res, head.Length); }

            if (tail != null)
            { Array.Copy(tail, 0, res, length - tail.Length, tail.Length); }

            return res;
        }

        public static char[] Line(int length, string fill, string? head = null, string? tail = null)
        { return Line(length, fill.ToCharArray(), head?.ToCharArray(), tail?.ToCharArray()); }
    }
}