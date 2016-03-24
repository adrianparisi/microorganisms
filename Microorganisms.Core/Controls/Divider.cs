using SomeTools;
using System.Drawing;

namespace Microorganisms.Core.Controls
{
    public class Divider : GrayButton
    {
        private Size cellSize;
        private Point leftCell;
        private Point rightCell;


        public Divider(Graphics graphics, Size size, Point center)
            : base(graphics, size)
        {
            this.Center = center;

            int spaceX = this.Size.Width / 12;
            int spaceY = this.Size.Width / 32;

            this.cellSize = this.Size.Divide(4);
            this.leftCell = this.Center - new Size(spaceX, spaceY) - cellSize;
            this.rightCell = this.Center + new Size(spaceX, spaceY);
        }

        #region Draw

        protected override void DrawContent()
        {
            this.DrawLine();
            this.DrawCell(this.leftCell);
            this.DrawCell(this.rightCell);
        }

        private void DrawLine()
        {
            int lenght = this.Size.Height / 3;
            Point top = this.Center + new Size(0, lenght * -1);
            Point down = this.Center + new Size(0, lenght);
            this.graphics.DrawLine(this.contentPen, top, down);
        }

        private void DrawCell(Point position1)
        {
            var rectangle = new Rectangle(position1, this.cellSize);
            this.graphics.DrawEllipse(this.contentPen, rectangle);
        }

        #endregion Draw
    }
}
