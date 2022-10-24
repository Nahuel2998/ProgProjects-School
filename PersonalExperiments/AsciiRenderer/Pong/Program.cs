using System;
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

            Paddle paddleLeft = new(
                texture: ']',
                x: 0,
                y: 5,
                direction: 1
            );

            Paddle paddleRight = new(
                texture: '[',
                x: 40,
                y: 5,
                direction: -1
            );

            board.DrawHorizontalLine(Shape.Line(board.XLength, '=', '#'), board.TopLeftXY);
            board.DrawHorizontalLine(Shape.Line(board.XLength, '=', '#'), board.BottomLeftXY);
            // board.DrawVerticalLine(Shape.Line(board.YLength, '|', '#'), 0, 0);
            // board.DrawVerticalLine(Shape.Line(board.YLength, '|', '#'), board.XLength - 1, 0);

            // BallFondler ballFondler = new(board, ball);
            FrameBallFondler frameBallFondler = new(board, ball);
            PaddlePaddler leftPaddler = new(board, paddleLeft, ball);
            PaddlePaddler rightPaddler = new(board, paddleRight, ball);
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
                leftPaddler.PaddlePaddle();
                rightPaddler.PaddlePaddle();

                board.PrintUpdated();
                // board.Print();

                Thread.Sleep(17);
            }
        }
    }
}