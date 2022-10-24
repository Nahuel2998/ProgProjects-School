namespace Pong
{
    public class FrameBall : IBall
    {
        public char Texture { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int DirectionX { get; set; }
        public int DirectionY { get; set; }
        public int MoveEveryX { get; set; }
        public int MoveEveryY { get; set; }
        public int CountX { get; set; }
        public int CountY { get; set; }

        public int RealX => X;
        public int RealY => Y;

        public FrameBall(char texture = '#', int x = 0, int y = 0, int directionX = 0, int directionY = 0, int moveEveryX = -1, int moveEveryY = -1)
        {
            Texture = texture;
            X = x;
            Y = y;
            DirectionX = directionX;
            DirectionY = directionY;
            MoveEveryX = moveEveryX;
            MoveEveryY = moveEveryY;
            CountX = MoveEveryX;
            CountY = MoveEveryY;
        }
    }
}