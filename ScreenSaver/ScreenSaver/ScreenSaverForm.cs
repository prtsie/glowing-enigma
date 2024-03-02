namespace ScreenSaver
{
    public partial class ScreenSaverForm : Form
    {
        private const int SnowflakesMaxCount = 75;
        private const float SizeFactorMultiplier = 5f;
        private const float SwingAmplitude = 5f;
        private const float SwingFrequencyMultiplier = 0.001f;
        private readonly int maxSnowflakeSize;
        private readonly int minSnowflakeSize;
        private readonly float averageSize;
        private readonly float snowflakeFallSpeed;
        private const string SourcePath = "Sources\\";
        private readonly Random random = new();
        private readonly List<Snowflake> snowflakes;
        private readonly List<Bitmap> images = new();
        private BufferedGraphics buffer;

        public ScreenSaverForm()
        {
            InitializeComponent();
            var screenSize = Screen.PrimaryScreen?.Bounds.Size;
            if (screenSize is null)
            {
                Close();
            }
            Size = screenSize!.Value;
            maxSnowflakeSize = (int)(Size.Height * 0.05f);
            minSnowflakeSize = maxSnowflakeSize / 3;
            averageSize = minSnowflakeSize + (maxSnowflakeSize - minSnowflakeSize) / 2;
            snowflakeFallSpeed = Size.Height * 0.1f * redrawer.Interval / 1000;
            buffer = null!;
            snowflakes = new();
            var imagesNames = Directory.GetFiles(SourcePath);
            foreach (var imageName in imagesNames)
            {
                images.Add(new Bitmap(imageName));
            }
            redrawer.Start();
            snowflakeGenerator.Start();
            Cursor.Hide();
        }

        private void MoveSnowflakes()
        {
            foreach (var snowflake in snowflakes)
            {
                var sizeFactor = SizeFactorMultiplier * (averageSize / snowflake.Image.Height);
                if (snowflake.Position.Y < Size.Height)
                {
                    snowflake.Move((int)(-Math.Cos(snowflake.Position.Y * SwingFrequencyMultiplier * sizeFactor) * SwingAmplitude), (int)(snowflakeFallSpeed + sizeFactor));
                }
                else
                {
                    snowflake.MoveTo(random.Next(Size.Width - snowflake.Image.Width), -snowflake.Image.Height);
                }
            }
        }

        private Bitmap ResizedImage(Bitmap image)
        {
            var size = random.Next(minSnowflakeSize, maxSnowflakeSize);
            return new Bitmap(image, size, size);
        }

        private void RedrawerTick(object _, EventArgs __)
        {
            MoveSnowflakes();
            buffer.Graphics.Clear(BackColor);
            foreach (var snowflake in snowflakes)
            {
                buffer.Graphics.DrawImage(snowflake.Image, snowflake.Position);
            }
            buffer.Render();
        }

        private void ScreenSaverForm_Shown(object _, EventArgs __)
        {
            buffer = BufferedGraphicsManager.Current.Allocate(CreateGraphics(), DisplayRectangle);
        }

        private void SnowflakeGeneratorTick(object _, EventArgs __)
        {
            if (snowflakes.Count < SnowflakesMaxCount)
            {
                var image = ResizedImage(images[random.Next(images.Count)]);
                var snowflake = new Snowflake(new Point(random.Next(Size.Width - image.Width), -image.Height), image);
                snowflakes.Add(snowflake);
            }
        }
    }
}
