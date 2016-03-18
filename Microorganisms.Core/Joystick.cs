using System.Drawing;

namespace Microorganisms.Core
{
    public class Joystick
    {
        private Graphics graphics;
        private bool visible;
        private Brush brush;
        private Size baseSize;
        private Size pointerSize;
        private Point baseCenter;
        private Point pointerCenter;
        private Point direction;


        private Point BasePosition
        {
            get
            {
                int x = this.baseCenter.X - this.baseSize.Width / 2;
                int y = this.baseCenter.Y - this.baseSize.Height / 2;

                return new Point(x, y);
            }
        }

        private Point PointerPosition
        {
            get
            {
                int x = this.pointerCenter.X - this.pointerSize.Width / 2;
                int y = this.pointerCenter.Y - this.pointerSize.Height / 2;

                return new Point(x, y);
            }
        }


        #region Initialization

        public Joystick(Graphics graphics)
        {
            this.graphics = graphics;
            this.InitializeGraphics();
        }

        private void InitializeGraphics()
        {
            this.visible = false;
            this.baseSize = new Size(60, 60);
            this.pointerSize = new Size(30, 30);
            this.brush = new SolidBrush(Color.FromArgb(64, 64, 64, 64));
        }

        #endregion Initialization
        
        #region Behaviour

        public void Enable(Point center)
        {
            this.baseCenter = center;
            this.direction = center;
            this.pointerCenter = this.GetIntersection();
            this.visible = true;
        }

        public void Disable()
        {
            this.visible = false;
        }

        public void Move(Point direction)
        {
            this.direction = direction;
            this.pointerCenter = this.GetIntersection();
        }

        /// <summary>
        /// Gets the intersection between the base circumference and the direction line.
        /// </summary>
        private Point GetIntersection()
        {
            // TODO calculate intersection

            return this.direction;
        }

        #endregion Behaviour

        #region Draw

        public void Draw()
        {
            if (this.visible)
            {
                this.Draw(this.BasePosition, this.baseSize);
                this.Draw(this.PointerPosition, this.pointerSize);
            }
        }

        private void Draw(Point position, Size size)
        {
            var rectangle = new Rectangle(position, size);
            this.graphics.FillEllipse(this.brush, rectangle);
        }

        #endregion Draw
    }
}
