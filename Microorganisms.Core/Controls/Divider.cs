using SomeTools;
using System.Drawing;

namespace Microorganisms.Core.Controls
{
    public class Divider : GrayButton
    {
        private Size cellSize;
        private Point leftCell;
        private Point rightCell;


        public Divider(Size size, Point center)
            : base(size)
        {
            this.Center = center;

            int spaceX = this.Size.Width / 12;
            int spaceY = this.Size.Width / 32;

            this.cellSize = this.Size.Divide(4);
            this.leftCell = this.Center - new Size(spaceX, spaceY) - cellSize;
            this.rightCell = this.Center + new Size(spaceX, spaceY);
        }

        #region Draw

        protected override void DrawContent(Graphics graphics)
        {
            this.DrawLine(graphics);
            this.DrawCell(graphics, this.leftCell);
            this.DrawCell(graphics, this.rightCell);
        }

        private void DrawLine(Graphics graphics)
        {
            int lenght = this.Size.Height / 3;
            Point top = this.Center + new Size(0, lenght * -1);
            Point down = this.Center + new Size(0, lenght);
            graphics.DrawLine(this.contentPen, top, down);
        }

        private void DrawCell(Graphics graphics, Point position1)
        {
            var rectangle = new Rectangle(position1, this.cellSize);
            graphics.DrawEllipse(this.contentPen, rectangle);
        }

        #endregion Draw
    }
}
