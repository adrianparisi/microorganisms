using System.Drawing;

namespace Microorganisms.Core.Controls
{
    class JoystickComponent
    {
        private Graphics graphics;
        private Brush brush;


        public Size Size { get; private set; }

        public int Radius
        {
            get { return this.Size.Width / 2; }
        }

        public Point Center { get; set; }

        public Point Position
        {
            get
            {
                int x = this.Center.X - this.Size.Width / 2;
                int y = this.Center.Y - this.Size.Height / 2;

                return new Point(x, y);
            }
        }


        #region Initialization

        public JoystickComponent(Graphics graphics, Size size)
        {
            this.graphics = graphics;
            this.InitializeGraphics(size);
        }

        private void InitializeGraphics(Size size)
        {
            this.Size = size;
            Color gray = Color.FromArgb(64, 64, 64, 64);
            this.brush = new SolidBrush(gray);
        }

        #endregion Initialization

        public void Draw()
        {
            var rectangle = new Rectangle(this.Position, this.Size);
            this.graphics.FillEllipse(this.brush, rectangle);
        }
    }
}
