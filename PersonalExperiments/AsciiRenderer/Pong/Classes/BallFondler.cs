using System;
using AsciiRenderer;
using NarExtensions;

namespace Pong
{
    public class BallFondler
    {
        public readonly Board Board;
        public readonly Ball Ball;

        public BallFondler(Board board, Ball ball)
        {
            Board = board;
            Ball = ball;

            Board.BoardMatrix[(int)Ball.Y, (int)Ball.X] = Ball.Texture;
        }

        // FIXME: This doesn't work well
        public void FondleBall()
        {
            (float, float) prevPos = (Ball.X, Ball.Y);
            (float, float) newPos = (Ball.GetNewX(), Ball.GetNewY());
            (int, int) realPrevPos = prevPos.AsIntTuple();
            (int, int) realNewPos = newPos.AsIntTuple();

            if (realPrevPos.Item1 != realNewPos.Item1 || realPrevPos.Item2 != realNewPos.Item2)
            {
                if (Board.Occupied(realNewPos))
                {
                    if (Board.Occupied(realNewPos.Item1, realPrevPos.Item2))
                    { Ball.VelocityX = -Ball.VelocityX; }
                    if (Board.Occupied(realPrevPos.Item1, realNewPos.Item2))
                    { Ball.VelocityY = -Ball.VelocityY; }

                    newPos = Ball.GetNewXY();
                    realNewPos = newPos.AsIntTuple();
                }

                Board.Move(realPrevPos, realNewPos);
            }

            Ball.X = newPos.Item1;
            Ball.Y = newPos.Item2;

            Console.WriteLine($"{Ball.X}, {Ball.Y}");
        }
    }
}