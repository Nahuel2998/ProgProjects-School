using AsciiRenderer;

namespace Pong
{
    public class FrameBallFondler
    {
        public readonly Board Board;
        public readonly FrameBall Ball;

        public FrameBallFondler(Board board, FrameBall ball)
        {
            Board = board;
            Ball = ball;

            Board.EditAt(Ball.X, Ball.Y, Ball.Texture);
        }

        public void FondleBall()
        {
            (int, int) previousPos = (Ball.X, Ball.Y);

            if (Ball.CountX-- == 0)
            {
                Ball.CountX = Ball.MoveEveryX;

                if (Board.Occupied(Ball.X + Ball.DirectionX, Ball.Y))
                { Ball.DirectionX *= -1; }

                previousPos.Item1 = Ball.X;
                Ball.X += Ball.DirectionX;
            }

            if (Ball.CountY-- == 0)
            {
                Ball.CountY = Ball.MoveEveryY;

                if (Board.Occupied(Ball.X, Ball.Y + Ball.DirectionY))
                { Ball.DirectionY = -Ball.DirectionY; }

                previousPos.Item2 = Ball.Y;
                Ball.Y += Ball.DirectionY;
            }

            if (Board.GetAt(Ball.X, Ball.Y) != Ball.Texture)
            { Board.Move(previousPos, (Ball.X, Ball.Y)); }
        }
    }
}