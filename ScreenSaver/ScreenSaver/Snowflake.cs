namespace ScreenSaver
{
    internal class Snowflake(Point pos, Bitmap image)
    {
        public Point Position { get; set; } = pos;

        public Bitmap Image { get; private set; } = image;

        public void Move(int horizontal, int vertical)
        {
            Position = new Point(Position.X + horizontal, Position.Y + vertical);
        }

        public void MoveTo(int x, int y)
        {
            Position = new Point(x, y);
        }
    }
}
