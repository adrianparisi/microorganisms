using System.Drawing;

namespace Microorganisms.Core
{
    public abstract class Microorganism
    {
        protected Graphics graphics;


        public int Mass { get; protected set; }

        public int Width
        {
            get { return this.Mass; }
        }

        public int Height
        {
            get { return this.Mass; }
        }

        public int Radius
        {
            get { return this.Width / 2; }
        }

        public Point Position { get; set; }

        public Point Center
        {
            get
            {
                int x = this.Position.X + this.Width / 2;
                int y = this.Position.Y + this.Height / 2;

                return new Point(x, y);
            }
        }


        public Microorganism(Graphics graphics, int mass)
        {
            this.graphics = graphics;
            this.Mass = mass;
        }

        public abstract void Draw(Size delta);
    }
}
