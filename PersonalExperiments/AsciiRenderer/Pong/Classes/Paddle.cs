namespace Pong
{
    public class Paddle
    {
        public char Texture { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Direction { get; set; }

        public Paddle(char texture = '|', int x = 0, int y = 0, int direction = 0)
        {
            Texture = texture;
            X = x;
            Y = y;
            Direction = direction;
        }
    }
}