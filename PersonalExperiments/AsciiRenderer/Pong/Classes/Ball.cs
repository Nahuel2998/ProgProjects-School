namespace Pong
{
    public class Ball
    {
        public char Texture { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }

        public Ball(char texture = '#', float x = 0, float y = 0, float velocityX = 0, float velocityY = 0)
        {
            Texture = texture;
            X = x;
            Y = y;
            VelocityX = velocityX;
            VelocityY = velocityY;
        }

        public float GetNewX()
        { return X + VelocityX; }

        public float GetNewY()
        { return Y + VelocityY; }

        public (float, float) GetNewXY()
        { return (GetNewX(), GetNewY()); }
    }
}