using System.Drawing;

namespace Microorganisms.Core.Controls
{
    public class Aim : GrayButton
    {
        public Aim(Graphics graphics, Size size)
            : base (graphics, size) { }

        protected override void DrawContent()
        {
            var rectangle = new Rectangle(this.Position, this.Size);
            rectangle.Inflate(this.Size.Width / -3, this.Size.Height / -3);
            this.graphics.DrawEllipse(this.contentPen, rectangle);

            int length = this.Size.Width / 6;
            var separation = this.Size.Width / 12;

            Point left1 = this.Center - new Size(length + separation, 0);
            Point left2 = this.Center - new Size(separation, 0);
            this.graphics.DrawLine(this.contentPen, left1, left2);

            Point right1 = this.Center + new Size(length + separation, 0);
            Point right2 = this.Center + new Size(separation, 0);
            this.graphics.DrawLine(this.contentPen, right1, right2);

            Point top1 = this.Center - new Size(0, length + separation);
            Point top2 = this.Center - new Size(0, separation);
            this.graphics.DrawLine(this.contentPen, top1, top2);

            Point down1 = this.Center + new Size(0, length + separation);
            Point down2 = this.Center + new Size(0, separation);
            this.graphics.DrawLine(this.contentPen, down1, down2);
        }
    }
}
