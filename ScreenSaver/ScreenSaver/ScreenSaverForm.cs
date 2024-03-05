using ScreenSaver.Properties;

namespace ScreenSaver
{
    public partial class ScreenSaverForm : Form
    {
        private const int SnowflakesMaxCount = 10;
        private readonly Random random = new();
        private readonly List<Snowflake> snowflakes;
        private readonly List<Bitmap> images;
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
            Snowflake.MaxSnowflakeSize = (int)(Size.Height * 0.1f);
            Snowflake.MinSnowflakeSize = Snowflake.MaxSnowflakeSize / 3;
            buffer = null!;
            snowflakes = new();
            images = new();
            var counter = 1;
            while (true)
            {
                if (Resources.ResourceManager.GetObject($"Snowflake{counter++}") is not Bitmap image)
                {
                    break;
                }
                images.Add(image);
            }
            for (var i = 0; i < SnowflakesMaxCount; i++)
            {
                var imageIndex = random.Next(images.Count);
                var snowflake = new Snowflake(imageIndex, new Point(random.Next(Size.Width - images[imageIndex].Width), -images[imageIndex].Height));
                snowflakes.Add(snowflake);
            }
            redrawer.Start();
            Cursor.Hide();
        }

        private void MoveSnowflakes()
        {
            foreach (var snowflake in snowflakes)
            {
                var image = images[snowflake.ImageIndex];
                if (snowflake.Area.Location.Y < Size.Height)
                {
                    snowflake.Move((int)(Math.Cos(snowflake.Area.Location.Y * Snowflake.SwingFrequencyMultiplier * snowflake.SwingSizeFactor) * Snowflake.SwingAmplitude),
                                   (int)(snowflake.FallingSpeed));
                }
                else
                {
                    snowflake.MoveTo(random.Next(Size.Width - image.Width), -image.Height);
                }
            }
        }

        private void RedrawerTick(object _, EventArgs __)
        {
            redrawer.Stop();
            MoveSnowflakes();
            buffer.Graphics.Clear(BackColor);
            foreach (var snowflake in snowflakes)
            {
                buffer.Graphics.DrawImage(images[snowflake.ImageIndex], snowflake.Area);
            }
            buffer.Render();
            redrawer.Start();
        }

        private void ScreenSaverForm_Shown(object _, EventArgs __)
        {
            buffer = BufferedGraphicsManager.Current.Allocate(CreateGraphics(), DisplayRectangle);
        }
    }
}
