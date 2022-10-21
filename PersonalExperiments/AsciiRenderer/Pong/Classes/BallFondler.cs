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
            float[] prevPos = new float[2] { Ball.X, Ball.Y };
            float[] newPos = new float[2] { Ball.GetNewX(), Ball.GetNewY() };
            int[] realPrevPos = prevPos.AsIntArray();
            int[] realNewPos = newPos.AsIntArray();

            if (realPrevPos[0] != realNewPos[0] || realPrevPos[1] != realNewPos[1])
            {
                if (Board.Occupied(realNewPos))
                {
                    if (Board.Occupied(realNewPos[0], realPrevPos[1]))
                    { Ball.VelocityX = -Ball.VelocityX; }
                    if (Board.Occupied(realPrevPos[0], realNewPos[1]))
                    { Ball.VelocityY = -Ball.VelocityY; }

                    newPos = Ball.GetNewXY();
                    realNewPos = newPos.AsIntArray();
                }

                Board.EditAt(realPrevPos, ' ');
                Board.EditAt(realNewPos, Ball.Texture);
            }

            Ball.X = newPos[0];
            Ball.Y = newPos[1];

            Console.WriteLine($"{Ball.X}, {Ball.Y}");
        }
    }
}