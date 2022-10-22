﻿using System;
using System.Threading;
using AsciiRenderer;

namespace Pong
{
    internal static class Program
    {
        static void Main()
        {
            QueueBoard board = new(41, 11);
            // Board board = new(41, 11);

            // Ball ball = new(
            //     x: 10,
            //     y: 5,
            //     velocityX: 0.2f,
            //     velocityY: 0.1f
            // );

            FrameBall ball = new(
                x: 20,
                y: 5,
                directionX: -1,
                directionY: -1,
                moveEveryX: 0,
                moveEveryY: 8
            );

            board.DrawHorizontalLine(Shape.Line(board.XLength, '=', '#'), 0, 0);
            board.DrawHorizontalLine(Shape.Line(board.XLength, '=', '#'), 0, board.YLength - 1);
            board.DrawVerticalLine(Shape.Line(board.YLength, '|', '#'), 0, 0);
            board.DrawVerticalLine(Shape.Line(board.YLength, '|', '#'), board.XLength - 1, 0);

            // BallFondler ballFondler = new(board, ball);
            FrameBallFondler frameBallFondler = new(board, ball);
            Console.CursorVisible = false;

            // while (true)
            // {
            //     Console.SetCursorPosition(0, 0);
            //     ballFondler.FondleBall();

            //     board.Print();

            //     Thread.Sleep(10);
            // }

            Console.SetCursorPosition(0, 0);
            board.Print();

            while (true)
            {
                // Console.SetCursorPosition(0, 0);
                frameBallFondler.FondleBall();

                board.PrintUpdated();
                // board.Print();

                Thread.Sleep(17);
            }
        }
    }
}