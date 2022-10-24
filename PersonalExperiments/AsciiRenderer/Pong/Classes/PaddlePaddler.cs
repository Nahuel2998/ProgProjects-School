using System;
using AsciiRenderer;

namespace Pong
{
    public class PaddlePaddler
    {
        public readonly Board Board;
        public readonly Paddle Paddle;
        public readonly IBall Ball;

        public PaddlePaddler(Board board, Paddle paddle, IBall ball)
        {
            Board = board;
            Paddle = paddle;
            Ball = ball;

            Board.EditAt(Paddle.X, Paddle.Y, Paddle.Texture);
        }

        public void PaddlePaddle()
        {
            (int, int) previousPos = (Paddle.X, Paddle.Y);

            if (Ball.DirectionX != Paddle.Direction)
            { Paddle.Y += Math.Sign(Ball.RealY - Paddle.Y); }

            if (Board.GetAt(Paddle.X, Paddle.Y) != Paddle.Texture)
            { Board.Move(previousPos, (Paddle.X, Paddle.Y)); }
        }
    }
}