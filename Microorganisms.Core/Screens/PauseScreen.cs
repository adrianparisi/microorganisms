using System.Drawing;

namespace Microorganisms.Core.Screens
{
    class PauseScreen : Screen
    {
        private static FilterSreen filter;


        public bool Enable { get; internal set; }


        public PauseScreen(Size size)
        {
            this.Size = size;
            Color gray = Color.FromArgb(128, 64, 64, 64);
            PauseScreen.filter = new FilterSreen(size, gray);
        }

        public void Draw(Graphics graphics)
        {
            if (!this.Enable)
                return;

            filter.Draw(graphics);

            string text = "PAUSE";
            Font font = new Font("Arial", 100);
            SolidBrush brush = new SolidBrush(Color.Black);
            var rectangle = new Rectangle(Point.Empty, this.Size);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            graphics.DrawString(text, font, brush, rectangle, format);

            font.Dispose();
            brush.Dispose();
            format.Dispose();
        }
    }
}
