using System.Drawing;

namespace Microorganisms.Core.Controls
{
    public class Aim
    {
        private Graphics graphics;
        private Brush backgroundBrush;
        private Pen aimPen;


        public Size Size { get; private set; }
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

        public Aim(Graphics graphics, Size size)
        {
            this.graphics = graphics;
            this.Size = size;

            this.InitializeGraphics();
        }

        private void InitializeGraphics()
        {
            Color gray = Color.FromArgb(64, 64, 64, 64);
            this.backgroundBrush = new SolidBrush(gray);
            this.aimPen = new Pen(Color.DarkGray, 2);
        }

        #endregion Initialization

        #region Draw

        public void Draw()
        {
            this.DrawBackground();
            this.DrawAim();
            this.DrawBorder();
        }

        private void DrawBackground()
        {
            var rectangle = new Rectangle(this.Position, this.Size);
            this.graphics.FillEllipse(this.backgroundBrush, rectangle);
        }

        private void DrawAim()
        {
            var rectangle = new Rectangle(this.Position, this.Size);
            rectangle.Inflate(this.Size.Width / -3, this.Size.Height / -3);
            this.graphics.DrawEllipse(this.aimPen, rectangle);

            int length = 11;
            var separation = 4;

            Point left1 = this.Center - new Size(length + separation, 0);
            Point left2 = this.Center - new Size(separation, 0);
            this.graphics.DrawLine(this.aimPen, left1, left2);

            Point right1 = this.Center + new Size(length + separation, 0);
            Point right2 = this.Center + new Size(separation, 0);
            this.graphics.DrawLine(this.aimPen, right1, right2);

            Point up1 = this.Center - new Size(0, length + separation);
            Point up2 = this.Center - new Size(0, separation);
            this.graphics.DrawLine(this.aimPen, up1, up2);

            Point down1 = this.Center + new Size(0, length + separation);
            Point down2 = this.Center + new Size(0, separation);
            this.graphics.DrawLine(this.aimPen, down1, down2);
        }

        private void DrawBorder()
        {
            var rectangle = new Rectangle(this.Position, this.Size);
            this.graphics.DrawEllipse(Pens.DarkGray, rectangle);
        }

        #endregion Draw
    }
}
