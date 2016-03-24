using SomeTools;
using System.Drawing;

namespace Microorganisms.Core.Controls
{
    public abstract class GrayButton
    {
        private Brush backgroundBrush;
        protected Graphics graphics;
        protected Pen contentPen;


        public Size Size { get; private set; }
        public Point Center { get; set; }

        public Point Position
        {
            get { return this.Center - this.Size.Divide(2); }
        }


        #region Initialization

        public GrayButton(Graphics graphics, Size size)
        {
            this.graphics = graphics;
            this.Size = size;

            this.InitializeGraphics();
        }

        private void InitializeGraphics()
        {
            Color gray = Color.FromArgb(64, 64, 64, 64);
            this.backgroundBrush = new SolidBrush(gray);
            this.contentPen = new Pen(Color.DarkGray, 2);
        }

        #endregion Initialization

        #region Draw

        public void Draw()
        {
            this.DrawBackground();
            this.DrawContent();
            this.DrawBorder();
        }

        private void DrawBackground()
        {
            var rectangle = new Rectangle(this.Position, this.Size);
            this.graphics.FillEllipse(this.backgroundBrush, rectangle);
        }

        private void DrawBorder()
        {
            var rectangle = new Rectangle(this.Position, this.Size);
            this.graphics.DrawEllipse(Pens.DarkGray, rectangle);
        }

        protected abstract void DrawContent();

        #endregion Draw
    }
}