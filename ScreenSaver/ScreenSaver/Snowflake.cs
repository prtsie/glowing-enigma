namespace ScreenSaver
{
    internal sealed class Snowflake
    {
        private const float SpeedSizeFactorMultiplier = 2f;
        private const float SwingSizeFactorMultiplier = 1f;
        private const float SpeedMultiplier = 5f;
        private readonly Random random = new();
        public static int MaxSnowflakeSize = 20;
        public static int MinSnowflakeSize = 70;
        public static float SwingFrequencyMultiplier = 0.007f;
        public static float SwingAmplitude = 5f;

        public Rectangle Area { get; private set; }

        public double FallingSpeed { get; private set; }

        public double SpeedSizeFactor { get; private set; }

        public double SwingSizeFactor { get; private set; }

        public int ImageIndex { get; private set; }

        public Snowflake(int imageIndex, Point location)
        {
            var size = random.Next(MinSnowflakeSize, MaxSnowflakeSize);
            Area = new Rectangle(location, new Size(size, size));
            ImageIndex = imageIndex;
            var averageSize = MinSnowflakeSize + (MaxSnowflakeSize - MinSnowflakeSize) / 2.0;
            var SizeFactor = averageSize / Area.Height;
            SpeedSizeFactor = SpeedSizeFactorMultiplier * SizeFactor;
            SwingSizeFactor = SwingSizeFactorMultiplier * SizeFactor;
            FallingSpeed = SpeedMultiplier + SpeedSizeFactor;
        }

        public void Move(int horizontal, int vertical)
        {
            Area = Area with { Location = new Point(Area.Location.X + horizontal, Area.Location.Y + vertical) };
        }

        public void MoveTo(int x, int y)
        {
            Area = Area with { Location = new Point(x, y) };
        }
    }
}
