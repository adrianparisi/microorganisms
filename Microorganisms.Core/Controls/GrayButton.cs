using SomeTools;
using System.Drawing;

namespace Microorganisms.Core.Controls
{
    public abstract class GrayButton
    {
        private Brush backgroundBrush;
        protected Pen contentPen;


        public Size Size { get; private set; }
        public Point Center { get; set; }

        public Point Position
        {
            get { return this.Center - this.Size.Divide(2); }
        }


        #region Initialization

        public GrayButton(Size size)
        {
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

        public void Draw(Graphics graphics)
        {
            this.DrawBackground(graphics);
            this.DrawContent(graphics);
            this.DrawBorder(graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            var rectangle = new Rectangle(this.Position, this.Size);
            graphics.FillEllipse(this.backgroundBrush, rectangle);
        }

        private void DrawBorder(Graphics graphics)
        {
            var rectangle = new Rectangle(this.Position, this.Size);
            graphics.DrawEllipse(Pens.DarkGray, rectangle);
        }

        protected abstract void DrawContent(Graphics graphics);

        #endregion Draw
    }
}